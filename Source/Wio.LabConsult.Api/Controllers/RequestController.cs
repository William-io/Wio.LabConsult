using MailKit.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Features.Addresses.CreateAddress;
using Wio.LabConsult.Application.Features.Addresses.Vms;
using Wio.LabConsult.Application.Features.Requests.Commands.CreateRequest;
using Wio.LabConsult.Application.Features.Requests.Commands.UpdateRequest;
using Wio.LabConsult.Application.Features.Requests.Queries.GetRequestsById;
using Wio.LabConsult.Application.Features.Requests.Queries.PaginationRequests;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Models.Authorization;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RequestController : ControllerBase
{
    private IMediator _mediator;
    private readonly IAuthService _authService;

    public RequestController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost("address", Name = "CreateAddress")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AddressVm>> CreateAddress([FromBody] CreateAddressCommand request)
    {
        return await _mediator.Send(request);
    }


    [HttpPost(Name = "CreateRequest")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<RequestVm>> CreateRequest([FromBody] CreateRequestCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut(Name = "UpdateRequest")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<RequestVm>> UpdateRequest([FromBody] UpdateRequestCommand request)
    {
        return await _mediator.Send(request);
    }


    [HttpGet("{id}", Name = "GetRequestById")]
    [ProducesResponseType(typeof(RequestVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RequestVm>> GetOrderById(int id)
    {
        var query = new GetRequestByIdQuery(id);
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("paginationByUsername", Name = "PaginationRequestByUsername")]
    [ProducesResponseType(typeof(PaginationVm<RequestVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<RequestVm>>> PaginationOrderByUsername
                                   (
                                       [FromQuery] PaginationRequestQuery paginationRequestParams
                                   )
    {
        paginationRequestParams.Username = _authService.GetSessionUser();
        var pagination = await _mediator.Send(paginationRequestParams);
        return Ok(pagination);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationRequest")]
    [ProducesResponseType(typeof(PaginationVm<RequestVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<RequestVm>>> PaginationOrder
                                    (
                                        [FromQuery] PaginationRequestQuery paginationRequestParams
                                    )
    {
        var pagination = await _mediator.Send(paginationRequestParams);
        return Ok(pagination);
    }
}
