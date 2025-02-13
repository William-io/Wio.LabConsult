using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Specifications.Users;

public class UserForCountingSpecification : BaseSpecification<User>
{
    public UserForCountingSpecification(UserSpecificationParams userParams) : base(
        x =>
        (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search)
         || x.LastName!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
        )
    )
    {
    }
}