using MediatR;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQuery : IRequest<AuthResponse>
{
    public string? UserName { get; set; }

    public GetUserByUserNameQuery(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
}