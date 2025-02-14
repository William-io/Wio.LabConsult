using MediatR;
using Wio.LabConsult.Application.Features.Appointments.VMs;

namespace Wio.LabConsult.Application.Features.Appointments.Commands.DeleteAppointmentItem;

public class DeleteAppointmentCommand : IRequest<AppointmentVm>
{
    public int Id { get; set; }
}
