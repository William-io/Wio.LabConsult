using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public UpdateUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());

        if (updateUser is null)
        {
            throw new BadRequestException("Usuario não existe!");
        }

        updateUser.Name = request.Name;
        updateUser.LastName = request.LastName;
        updateUser.PhoneNumber = request.Phone;
        updateUser.AvatarUrl = request.PhotoUrl ?? updateUser.AvatarUrl;

        var result = await _userManager.UpdateAsync(updateUser);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Erro ao atualizar usuário!");
        }

        var userById = await _userManager.FindByEmailAsync(request.Email!);
        var roles = await _userManager.GetRolesAsync(userById!);

        return new AuthResponse
        {
            Id = updateUser!.Id,
            Name = updateUser.Name,
            LastName = updateUser.LastName,
            Phone = updateUser.PhoneNumber,
            Email = updateUser.Email,
            Username = updateUser.UserName,
            Avatar = updateUser.AvatarUrl,
            Token = _authService.CreateToken(userById, roles),
            Roles = roles.ToList()
        };
    }
}
