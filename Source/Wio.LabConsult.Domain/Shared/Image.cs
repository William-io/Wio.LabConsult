using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Domain.Shared;

public class Image : Entity
{
    public string? Url { get; set; }
    public int ConsultId { get; set; }
    public string? PublicCode { get; set; }
    public virtual Consult? Consult { get; set; }
}
