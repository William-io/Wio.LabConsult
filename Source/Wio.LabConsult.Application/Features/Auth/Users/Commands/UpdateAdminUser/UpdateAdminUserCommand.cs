using MediatR;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateAdminUser;

public class UpdateAdminUserCommand : IRequest<User>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Role { get; set; }
}