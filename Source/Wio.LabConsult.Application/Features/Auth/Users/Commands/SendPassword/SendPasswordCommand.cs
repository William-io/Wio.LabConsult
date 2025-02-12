using MediatR;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.SendPassword;

public class SendPasswordCommand : IRequest<string>
{
    public string? Email { get; set; }
}
