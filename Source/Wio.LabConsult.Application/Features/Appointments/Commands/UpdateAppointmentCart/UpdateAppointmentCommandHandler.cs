using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Exceptions;
using Wio.LabConsult.Application.Features.Appointments.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Appointments;

namespace Wio.LabConsult.Application.Features.Appointments.Commands.UpdateAppointmentCart;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAppointmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentVm> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointmentToUpdate = await _unitOfWork.Repository<Appointment>().GetEntityAsync(
            p => p.AppointmentCartMasterId == request.AppointmentId
        );

        if (appointmentToUpdate is null)
        {
            throw new NotFoundException(nameof(Appointment), request.AppointmentId!);
        }

        var appointmentItems = await _unitOfWork.Repository<AppointmentItem>().GetAsync(
            x => x.AppointmentCartMasterId == request.AppointmentId
        );

        _unitOfWork.Repository<AppointmentItem>().DeleteRange(appointmentItems);

        var appointmentItemsToAdd = _mapper.Map<List<AppointmentItem>>(request.AppointmentItems);

        appointmentItemsToAdd.ForEach(x =>
        {
            x.AppointmentCartId = appointmentToUpdate.Id;
            x.AppointmentCartMasterId = request.AppointmentId;
        });

        _unitOfWork.Repository<AppointmentItem>().AddRange(appointmentItemsToAdd);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            throw new Exception("Falha ao adicionar itens da consulta ao carrinho de compras");
        }

        var includes = new List<Expression<Func<Appointment, object>>>();
        includes.Add(p => p.AppointmentItems!.OrderBy(x => x.Consult));
        var basketCart = await _unitOfWork.Repository<Appointment>().GetEntityAsync(
            x => x.AppointmentCartMasterId == request.AppointmentId,
            includes,
            true
        );

        return _mapper.Map<AppointmentVm>(basketCart);

    }
}
