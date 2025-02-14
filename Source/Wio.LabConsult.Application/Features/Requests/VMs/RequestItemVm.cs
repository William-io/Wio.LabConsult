namespace Wio.LabConsult.Application.Features.Requests.VMs;

public class RequestItemVm
{
    public int ConsultId { get; set; }

    public decimal Price { get; set; }

    public DateTime DateRequest { get; set; }

    public int Quantity { get; set; }

    public int RequestId { get; set; }

    public int ConsultItemId { get; set; }

    public string? ConsultName { get; set; }

    public string? ImagenUrl { get; set; }
}
