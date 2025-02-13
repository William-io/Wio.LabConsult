using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Features.Countries.Queries.GetCountryList;
using Wio.LabConsult.Application.Features.Countries.VMs;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CountryController : ControllerBase
{
    private IMediator _mediator;

    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetCountries")]
    [ProducesResponseType(typeof(IReadOnlyList<CountryVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<CountryVm>>> GetCountries()
    {
        var query = new GetCountryListQuery();
        return Ok(await _mediator.Send(query));
    }

}
