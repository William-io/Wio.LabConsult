using MediatR;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.ResetPasswordByToken;

public class ResetPasswordByTokenCommand : IRequest<string>
{
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
