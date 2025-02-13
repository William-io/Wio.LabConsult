using MediatR;
using Microsoft.AspNetCore.Identity;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, AuthResponse>
{
    protected readonly UserManager<User> _userManager;

    public GetUserByUserNameQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResponse> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName!);

        if (user is null)
        {
            throw new Exception("Usuario não existe!");
        }

        return new AuthResponse
        {
            Id = user.Id,
            Name = user.UserName,
            LastName = user.UserName,
            Phone = user.PhoneNumber,
            Username = user.UserName,
            Email = user.Email,
            Avatar = user.AvatarUrl,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
}