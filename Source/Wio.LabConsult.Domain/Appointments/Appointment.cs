using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Appointments;

public class Appointment : Entity
{
    public Guid? AppointmentMasterId { get; set; }

    public virtual ICollection<AppointmentItem>? AppointmentItems { get; set; }
}
