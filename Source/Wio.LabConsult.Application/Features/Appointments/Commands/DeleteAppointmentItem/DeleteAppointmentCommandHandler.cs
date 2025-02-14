using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Features.Appointments.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Appointments;

namespace Wio.LabConsult.Application.Features.Appointments.Commands.DeleteAppointmentItem;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, AppointmentVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteAppointmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentVm> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointmentItem = await _unitOfWork.Repository<AppointmentItem>().GetEntityAsync(
            x => x.Id == request.Id
        );

        await _unitOfWork.Repository<AppointmentItem>().DeleteAsync(appointmentItem);


        var includes = new List<Expression<Func<Appointment, object>>>();
        includes.Add(p => p.AppointmentItems!.OrderBy(x => x.Consult));

        var appointment = await _unitOfWork.Repository<Appointment>().GetEntityAsync(
            x => x.AppointmentCartMasterId == appointmentItem.AppointmentCartMasterId,
            includes,
            true
        );

        return _mapper.Map<AppointmentVm>(appointment);
    }
}
