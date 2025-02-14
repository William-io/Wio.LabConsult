using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;

namespace Wio.LabConsult.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<RequestVm>
{
    public int RequestId { get; set; }
    public Guid? AppointmentCartMasterId { get; set; }
}
