using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Features.Requests.Commands.UpdateRequest;

public class UpdateRequestCommand : IRequest<RequestVm>
{
    public int RequestId { get; set; }
    public RequestStatus Status { get; set; }
}
