using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Images.Queries.Vms;

public class ImageVm
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public int ConsultId { get; set; }
    public string? PublicCode { get; set; }
}
