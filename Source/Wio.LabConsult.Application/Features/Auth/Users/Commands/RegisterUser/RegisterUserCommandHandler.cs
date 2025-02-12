
using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userByEmailExisting = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;
        if (userByEmailExisting)
        {
            throw new BadRequestException("User existente na base de dados!");
        }

        var userByUserNameExisting = await _userManager.FindByNameAsync(request.Username!) is null ? false : true;

        var user = new User
        {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Username,
            Phone = request.Phone,
            AvatarUrl = request.PhotoUrl
        };

        var result = await _userManager.CreateAsync(user, request.Password!);

        if (result.Succeeded) 
        {
            await _userManager.AddToRoleAsync(user, Role.GenericUser);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Token = _authService.CreateToken(user, roles),
                Roles = roles
            };
        }

        throw new BadRequestException("Erro ao criar usuário!");
    }
}
