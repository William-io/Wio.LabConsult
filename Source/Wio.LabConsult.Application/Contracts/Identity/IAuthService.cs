using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Contracts.Identity;

public interface IAuthService
{
    string GetSessionUser();

    string CreateToken(User user, IList<string>? roles);
}
