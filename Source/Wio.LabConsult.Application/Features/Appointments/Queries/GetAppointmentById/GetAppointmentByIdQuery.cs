using MediatR;
using Wio.LabConsult.Application.Features.Appointments.VMs;

namespace Wio.LabConsult.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQuery : IRequest<AppointmentVm>
{
    public Guid? AppointmentId { get; set; }

    public GetAppointmentByIdQuery(Guid? appointmentId)
    {
        AppointmentId = appointmentId ?? throw new ArgumentNullException(nameof(appointmentId));
    }
}
