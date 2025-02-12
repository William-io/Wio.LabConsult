using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(UserManager<User> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());

        if (updateUser is null)
        {
            throw new BadRequestException("Usuario não existe!");
        }

        var resultValidateOldPassword = _userManager.PasswordHasher.VerifyHashedPassword(updateUser, updateUser.PasswordHash!, request.OldPassword!);

        if (!(resultValidateOldPassword == PasswordVerificationResult.Success))
        {
            throw new BadRequestException("Senha antiga inválida!");
        }

        var newPasswordHash = _userManager.PasswordHasher.HashPassword(updateUser, request.NewPassword!);

        updateUser.PasswordHash = newPasswordHash;

        var result = await _userManager.UpdateAsync(updateUser);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Erro ao atualizar senha!");
        }

        return Unit.Value;
    }
}
