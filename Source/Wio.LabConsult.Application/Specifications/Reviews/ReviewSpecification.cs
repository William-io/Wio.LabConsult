using Wio.LabConsult.Domain.Reviews;

namespace Wio.LabConsult.Application.Specifications.Reviews;

public class ReviewSpecification : BaseSpecification<Review>
{
    public ReviewSpecification(ReviewSpecificationParams reviewParams)
        : base(
            x =>
            (!reviewParams.ConsultId.HasValue || x.ConsultId == reviewParams.ConsultId)
        )
    {
        ApplyPaging(reviewParams.PageSize * (reviewParams.PageIndex - 1), reviewParams.PageSize);

        if (!string.IsNullOrEmpty(reviewParams.Sort))
        {
            switch (reviewParams.Sort)
            {
                case "createDateAsc":
                    AddOrderBy(p => p.CreatedDate!);
                    break;

                case "createDateDesc":
                    AddOrderByDescending(p => p.CreatedDate!);
                    break;

                default:
                    AddOrderBy(p => p.CreatedDate!);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(p => p.CreatedDate!);
        }
    }
}