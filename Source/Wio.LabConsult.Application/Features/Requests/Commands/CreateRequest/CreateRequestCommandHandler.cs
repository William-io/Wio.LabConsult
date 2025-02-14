using AutoMapper;
using MailKit.Search;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Models.Payment;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Appointments;
using Wio.LabConsult.Domain.Requests;
using Wio.LabConsult.Domain.Users;
using Address = Wio.LabConsult.Domain.Shared.Address;

namespace Wio.LabConsult.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, RequestVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;
    private readonly StripeSettings _stripeSettings;

    public CreateRequestCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IAuthService authService, 
        UserManager<User> userManager,
         IOptions<StripeSettings> stripeSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
        _userManager = userManager;
        _stripeSettings = stripeSettings.Value;
    }

    public async Task<RequestVm> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        bool disabledTracker = true;
        var requestPending = await _unitOfWork.Repository<Request>().GetEntityAsync(
            requestPeding => requestPeding.PatientUserName == _authService.GetSessionUser() && requestPeding.Status == RequestStatus.Pending,
            null, disabledTracker);

        if (requestPending is not null)
            await _unitOfWork.Repository<Request>().DeleteAsync(requestPending);

        //Definição de carregamento antecipado, onde AppointmentItems é incluido a lista da expressão sendo ordenado pela consulta.
        var includes = new List<Expression<Func<Appointment, object>>>();
        includes.Add(i => i.AppointmentItems!.OrderBy(i => i.Consult));

        var appointment = await _unitOfWork.Repository<Appointment>().GetEntityAsync(
           x => x.AppointmentCartMasterId == request.AppointmentId,
           includes,
           false
        );

        var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
        if (user is null)
        {
            throw new Exception("Usuario não autenticado!");
        }

        var confimation = await _unitOfWork.Repository<Address>().GetEntityAsync(
           x => x.Username == user.UserName,
           null,
           false
        );

        RequestConfirmation requestConfirmation = new()
        {
            Email = confimation.Email,
            Phone = confimation.Phone,
            Cpf = confimation.Cpf,
            Username = user.UserName
        };

        await _unitOfWork.Repository<RequestConfirmation>().AddAsync(requestConfirmation);

        var subtotal = Math.Round(appointment.AppointmentItems!.Sum(x => x.Price * x.Quantity), 2);
        var rate = Math.Round(subtotal * Convert.ToDecimal(0.18), 2);
        var priceWithoutPlan = subtotal < 100 ? 10 : 25;
        var total = subtotal + rate + priceWithoutPlan;

        var namePatient = $"{user.Name}  {user.LastName}";
        var requestPatient = new Request(namePatient, user.UserName!, requestConfirmation, subtotal, total, rate, priceWithoutPlan);

        await _unitOfWork.Repository<Request>().AddAsync(requestPatient);

        var items = new List<RequestItem>();

        foreach (var appointmentElement in appointment.AppointmentItems!)
        {
            var requestItem = new RequestItem
            {
                ConsultName = appointmentElement.Consult,
                ConsultId = appointmentElement.ConsultId,
                ImageUrl = appointmentElement.Image,
                Price = appointmentElement.Price,
                Quantity = appointmentElement.Quantity,
                RequestId = requestPatient.Id
            };

            items.Add(requestItem);
        }

        _unitOfWork.Repository<RequestItem>().AddRange(items);

        var resultado = await _unitOfWork.Complete();

        if (resultado <= 0)
        {
            throw new Exception("Error em criar o agendamento!");
        }

        #region STRIPE_CONFIG_PAYMENT
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        var service = new PaymentIntentService();
        PaymentIntent intent;

        if (string.IsNullOrEmpty(requestPatient.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)requestPatient.Total,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            intent = await service.CreateAsync(options);
            requestPatient.PaymentIntentId = intent.Id;
            requestPatient.ClientSecret = intent.ClientSecret;
            requestPatient.StripeApiKey = _stripeSettings.PublishbleKey;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)requestPatient.Total
            };
            await service.UpdateAsync(requestPatient.PaymentIntentId, options);
        }
        #endregion

        _unitOfWork.Repository<Request>().UpdateEntity(requestPatient);
        var resultadoOrder = await _unitOfWork.Complete();

        if (resultadoOrder <= 0)
        {
            throw new Exception("Error creando el payment intent en stripe");
        }

        return _mapper.Map<RequestVm>(requestPatient);

    }
}
