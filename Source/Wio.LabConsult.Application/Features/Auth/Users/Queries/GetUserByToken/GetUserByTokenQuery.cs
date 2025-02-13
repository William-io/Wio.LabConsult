using MediatR;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.GetUserByToken;

public class GetUserByTokenQuery : IRequest<AuthResponse>
{
}