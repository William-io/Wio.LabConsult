using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Specifications.Consults;

public class ConsultForCountingSpecification : BaseSpecification<Consult>
{
    public ConsultForCountingSpecification(ConsultSpecificationParams consultParams)
        : base(
            x => (string.IsNullOrEmpty(consultParams.Search) || x.Name!.Contains(consultParams.Search)
                || x.Description!.Contains(consultParams.Search)
            ) &&
            (!consultParams.CategoryId.HasValue || x.CategoryId == consultParams.CategoryId) &&
            (!consultParams.Status.HasValue || x.Status == consultParams.Status))
    {
    }

}
