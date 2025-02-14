using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;

namespace Wio.LabConsult.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommand : IRequest<RequestVm>
{
    public Guid? AppointmentId { get; set; }
}
