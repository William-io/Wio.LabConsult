using Microsoft.IdentityModel.Tokens;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Specifications.Requests;

public class RequestSpecification : BaseSpecification<Request>
{
    public RequestSpecification(RequestSpecificationParams requestParams)
          : base(x =>
                (string.IsNullOrEmpty(requestParams.Username) ||
                x.PatientName!.Contains(requestParams.Username)) &&
                (!requestParams.Id.HasValue || x.Id == requestParams.Id))
    {
        AddInclude(p => p.RequestItems!);

        ApplyPaging(requestParams.PageSize * (requestParams.PageIndex - 1), requestParams.PageSize);

        if (!string.IsNullOrEmpty(requestParams.Sort))
        {

            switch (requestParams.Sort)
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
