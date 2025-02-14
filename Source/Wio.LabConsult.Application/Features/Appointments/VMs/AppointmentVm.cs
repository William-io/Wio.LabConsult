namespace Wio.LabConsult.Application.Features.Appointments.VMs;

public class AppointmentVm
{
    public string? AppointmenttId { get; set; }

    public List<AppointmentItemVm>? AppointmentItems { get; set; }

    public decimal Total
    {
        get
        {
            return
                    Math.Round(
                        AppointmentItems!.Sum(x => x.Price * x.Amount) +
                        AppointmentItems!.Sum(x => x.Price * x.Amount) * Convert.ToDecimal(0.18) +
                        (AppointmentItems!.Sum(x => x.Price * x.Amount) < 100 ? 10 : 25)
                    , 2
                    );
        }

        set { }
    }

    public int Cantidad
    {
        get { return AppointmentItems!.Sum(x => x.Amount); }
        set { }
    }

    public decimal SubTotal
    {
        get { return Math.Round(AppointmentItems!.Sum(x => x.Price * x.Amount), 2); }
    }

    //public decimal Aplicar taxa de plano de saude.
    //{
    //    get
    //    {
    //        return Math.Round(((BasketCartItems!.Sum(x => x.Price * x.Amount)) * Convert.ToDecimal(0.18)), 2);
    //    }
    //    set { }
    //}

    //public decimal Aplicar desconto quando há plano da clinica
    //{
    //    get
    //    {
    //        return (BasketCartItems!.Sum(x => x.Price * x.Amount)) < 100 ? 10 : 25;
    //    }

    //    set { }
    //}
}
