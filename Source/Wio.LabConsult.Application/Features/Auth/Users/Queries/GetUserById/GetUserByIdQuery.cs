using MediatR;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<AuthResponse>
{
    public GetUserByIdQuery(string userId)
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    public string? UserId { get; set; }
}