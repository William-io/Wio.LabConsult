using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Appointments;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, RequestVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RequestVm> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var requestToPay = await _unitOfWork.Repository<Request>().GetEntityAsync(
            x => x.Id == request.RequestId,
            null,
            false
        );

        requestToPay.Status = RequestStatus.Confirmed;
        _unitOfWork.Repository<Request>().UpdateEntity(requestToPay);

        var AppointmentCartItems = await _unitOfWork.Repository<AppointmentItem>().GetAsync(
            x => x.AppointmentCartMasterId == request.AppointmentCartMasterId
        );

        _unitOfWork.Repository<AppointmentItem>().DeleteRange(AppointmentCartItems);

        await _unitOfWork.Complete();

        return _mapper.Map<RequestVm>(requestToPay);
    }
}
