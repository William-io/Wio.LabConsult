using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Features.Reviews.Commands.CreateReview;
using Wio.LabConsult.Application.Features.Reviews.Commands.DeleteReview;
using Wio.LabConsult.Application.Features.Reviews.Queries.PaginationReviews;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Models.Authorization;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewController : ControllerBase
{
    private IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "CreateReview")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ReviewVm>> CreateReview([FromBody] CreateReviewCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("{id}", Name = "DeleteReview")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Unit>> DeleteReview(int id)
    {
        var request = new DeleteReviewCommand(id);
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationReviews", Name = "PaginationReview")]
    [ProducesResponseType(typeof(PaginationVm<ReviewVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Unit>> PaginationReview([FromQuery] PaginationReviewsQuery request)
    {
        var paginationReview = await _mediator.Send(request);
        return Ok(paginationReview);
    }
}
