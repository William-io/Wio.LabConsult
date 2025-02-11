using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Specifications.Consults;

public class ConsultSpecification : BaseSpecification<Consult>
{
    public ConsultSpecification(ConsultSpecificationParams consultParams)
        : base(
            x => (string.IsNullOrEmpty(consultParams.Search) || x.Name!.Contains(consultParams.Search)
                || x.Description!.Contains(consultParams.Search)
            ) &&
            (!consultParams.CategoryId.HasValue || x.CategoryId == consultParams.CategoryId) &&
            (!consultParams.Status.HasValue || x.Status == consultParams.Status))
    {
        AddInclude(c => c.Reviews!);
        AddInclude(c => c.Images!);

        ApplyPaging(consultParams.PageSize * (consultParams.PageIndex - 1), consultParams.PageSize);

        if (!string.IsNullOrEmpty(consultParams.Sort))
        {
            switch (consultParams.Sort)
            {
                case "nameAsc":
                    AddOrderBy(p => p.Name!);
                    break;
                case "nameDesc":
                    AddOrderByDescending(p => p.Name!);
                    break;
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                case "ratingAsc":
                    AddOrderBy(p => p.Rating);
                    break;
                case "RatingDesc":
                    AddOrderByDescending(p => p.Rating);
                    break;
                default:
                    AddOrderBy(p => p.CreatedDate!);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(c => c.CreatedDate!);
        }
    }
}
