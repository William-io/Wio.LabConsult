namespace Wio.LabConsult.Application.Features.Appointments.VMs;

public class AppointmentItemVm
{
    public int Id { get; set; }

    public int ConsultId { get; set; }
    public string? Consult { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }

    public string? Category { get; set; }
    public int Availability { get; set; }

    public decimal TotalLine
    {
        get
        {
            return Math.Round(Amount * Price, 2);
        }

        set { }
    }
}
