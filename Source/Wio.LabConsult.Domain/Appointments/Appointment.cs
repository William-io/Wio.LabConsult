using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Appointments;

public class Appointment : Entity
{
    public Guid? AppointmentCartMasterId { get; set; }

    public virtual ICollection<AppointmentItem>? AppointmentItems { get; set; }
}
