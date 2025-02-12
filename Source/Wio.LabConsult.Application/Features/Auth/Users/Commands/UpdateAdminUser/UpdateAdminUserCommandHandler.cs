using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateAdminUser;

public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, User>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public UpdateAdminUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    public async Task<User> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.Id!);

        if (updateUser is null)
        {
            throw new BadRequestException("Usuario não existe!");
        }

        updateUser.Name = request.Name;
        updateUser.LastName = request.LastName;
        updateUser.Phone = request.Phone;

        var result = await _userManager.UpdateAsync(updateUser);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Erro ao atualizar usuário!");
        }

        var role = await _roleManager.FindByNameAsync(request.Role!);

        if (role is null)
        {
            throw new BadRequestException("Perfil não existe!");
        }

        await _userManager.AddToRoleAsync(updateUser, role.Name!);

        return updateUser;
    }
}