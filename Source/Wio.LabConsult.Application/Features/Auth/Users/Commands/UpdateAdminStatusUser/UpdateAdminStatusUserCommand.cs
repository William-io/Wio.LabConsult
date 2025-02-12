using MediatR;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateAdminStatusUser;

public class UpdateAdminStatusUserCommand : IRequest<User>
{
    public string? Id { get; set; }
}