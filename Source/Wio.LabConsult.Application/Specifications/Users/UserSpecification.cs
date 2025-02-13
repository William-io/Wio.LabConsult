using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Specifications.Users;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(UserSpecificationParams userParams) : base(
       x =>
       (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search)
        || x.LastName!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
       )
   )
    {
        ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);

        if (!string.IsNullOrEmpty(userParams.Sort))
        {
            switch (userParams.Sort)
            {
                case "nombreAsc":
                    AddOrderBy(p => p.Name!);
                    break;

                case "nombreDesc":
                    AddOrderByDescending(p => p.Name!);
                    break;

                case "apellidoAsc":
                    AddOrderBy(p => p.LastName!);
                    break;

                case "apellidoDesc":
                    AddOrderByDescending(p => p.LastName!);
                    break;

                default:
                    AddOrderBy(p => p.Name!);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(p => p.Name!);
        }
    }

}