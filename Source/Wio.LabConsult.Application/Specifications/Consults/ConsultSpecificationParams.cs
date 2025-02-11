using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Specifications.Consults;

public class ConsultSpecificationParams : SpecificationParams
{
    public int? CategoryId { get; set; }
    public int? Rating { get; set; }
    public ConsultStatus? Status { get; set; }
}
