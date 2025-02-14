using MediatR;
using Wio.LabConsult.Application.Features.Appointments.VMs;

namespace Wio.LabConsult.Application.Features.Appointments.Commands.UpdateAppointmentCart;

public class UpdateAppointmentCommand : IRequest<AppointmentVm>
{
    public Guid? AppointmentId { get; set; }

    public List<AppointmentItemVm>? AppointmentItems { get; set; }
}
