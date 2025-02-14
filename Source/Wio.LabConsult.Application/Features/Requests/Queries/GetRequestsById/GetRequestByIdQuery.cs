using MailKit.Search;
using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;

namespace Wio.LabConsult.Application.Features.Requests.Queries.GetRequestsById;

public class GetRequestByIdQuery : IRequest<RequestVm>
{
    public int RequestId { get; set; }

    public GetRequestByIdQuery(int requestId)
    {
        this.RequestId = requestId == 0 ? throw new ArgumentNullException(nameof(requestId)) : requestId;
    }
}
