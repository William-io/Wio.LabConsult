using MailKit.Search;
using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;

namespace Wio.LabConsult.Application.Features.Requests.Queries.PaginationRequests;

public class PaginationRequestQuery : PaginationBaseQuery, IRequest<PaginationVm<RequestVm>>
{
    public int? Id { get; set; }
    public string? Username { get; set; }
}
