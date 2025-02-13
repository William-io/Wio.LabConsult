using Wio.LabConsult.Domain.Reviews;

namespace Wio.LabConsult.Application.Specifications.Reviews;

public class ReviewForCountingSpecification : BaseSpecification<Review>
{
    public ReviewForCountingSpecification(ReviewSpecificationParams reviewParams)
       : base(
           x =>
           (!reviewParams.ConsultId.HasValue || x.ConsultId == reviewParams.ConsultId)
       )
    {
    }

}