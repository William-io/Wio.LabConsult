using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Appointments;

public class AppointmentItem : Entity
{
    public string? Consult { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Image { get; set; }
    public string? Category { get; set; }

    public Guid? AppointmentCartMasterId { get; set; }

    public int AppointmentCartId { get; set; }
    public virtual Appointment? Appointment { get; set; }


    public int ConsultId { get; set; }
    public int Availability { get; set; } //stock
}
