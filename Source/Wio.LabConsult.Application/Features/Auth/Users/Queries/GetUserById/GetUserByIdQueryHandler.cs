using MediatR;
using Microsoft.AspNetCore.Identity;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
{
    private readonly UserManager<User> _userManager;

    public GetUserByIdQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId!);

        if (user is null)
        {
            throw new Exception("Usuario não existe!");
        }

        return new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Phone = user.PhoneNumber,
            Email = user.Email,
            Username = user.UserName,
            Avatar = user.AvatarUrl,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
}