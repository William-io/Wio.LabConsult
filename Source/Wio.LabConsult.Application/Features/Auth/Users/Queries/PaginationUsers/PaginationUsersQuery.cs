using MediatR;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.PaginationUsers;
public class PaginationUsersQuery : PaginationBaseQuery, IRequest<PaginationVm<User>>
{
}