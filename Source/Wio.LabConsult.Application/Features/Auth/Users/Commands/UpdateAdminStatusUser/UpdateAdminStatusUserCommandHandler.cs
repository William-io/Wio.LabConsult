using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateAdminStatusUser;

public class UpdateAdminStatusUserCommandHandler : IRequestHandler<UpdateAdminStatusUserCommand, User>
{
    private readonly UserManager<User> _userManager;

    public UpdateAdminStatusUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.Id!);

        if (updateUser is null)
        {
            throw new BadRequestException("Usuario não existe!");
        }

        updateUser.IsActive = !updateUser.IsActive;

        var result = await _userManager.UpdateAsync(updateUser);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Erro ao atualizar usuário!");
        }

        return updateUser;
    }
}