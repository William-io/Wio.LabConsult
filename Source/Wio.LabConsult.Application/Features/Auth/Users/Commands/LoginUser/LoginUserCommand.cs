using MediatR;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<AuthResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
