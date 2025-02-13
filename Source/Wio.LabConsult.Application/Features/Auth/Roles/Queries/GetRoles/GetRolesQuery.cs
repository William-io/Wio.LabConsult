using MediatR;

namespace Wio.LabConsult.Application.Features.Auth.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<string>>
{
}