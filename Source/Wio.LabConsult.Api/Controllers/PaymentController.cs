using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Features.Payments.Commands.CreatePayment;
using Wio.LabConsult.Application.Features.Requests.VMs;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "CreatePayment")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<RequestVm>> CreatePayment(
            [FromBody] CreatePaymentCommand request
    )
    {
        return await _mediator.Send(request);
    }

}
