using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using System.Text;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.ResetPasswordByToken;

public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
{
    private readonly UserManager<User> _userManager;

    public ResetPasswordByTokenCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
    {
        if (!string.Equals(request.Password, request.ConfirmPassword))
        {
            throw new BadRequestException("A Senha precisa ser igual na confimação!");
        }

        var updateUser = await _userManager.FindByEmailAsync(request.Email!);
        if (updateUser is null)
        {
            throw new BadRequestException("Email não registrado!");
        }

        var token = Convert.FromBase64String(request.Token!);
        var tokenResult = Encoding.UTF8.GetString(token);

        var resetResult = await _userManager.ResetPasswordAsync(updateUser!, tokenResult, request.Password!);

        if (!resetResult.Succeeded)
        {
            throw new Exception("Erro ao resetar a senha!");
        }

        return $"Senha resetada com sucesso! {request.Email}";
    }
}
