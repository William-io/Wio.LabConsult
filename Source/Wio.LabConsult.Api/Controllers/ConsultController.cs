using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Features.Consults.Queries.GetConsultList;
using Wio.LabConsult.Application.Features.Consults.Queries.PaginationConsults;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ConsultController : ControllerBase
{
    private IMediator _mediator;

    public ConsultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/v1/Consult
    [AllowAnonymous]
    [HttpGet("list", Name = "GetConsultList")]
    [ProducesResponseType(typeof(IReadOnlyList<ConsultVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ConsultVm>>> GetConsultList()
    {
        var query = new GetConsultListQuery();
        var consults = await _mediator.Send(query); //Enviar um objeto de consulta para o manipulador
        //Handler processa a consulta e retorna o resultado
        return Ok(consults);
    }

    [AllowAnonymous]
    [HttpGet("pagination", Name = "PaginationConsult")]
    [ProducesResponseType(typeof(PaginationVm<ConsultVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ConsultVm>>> PaginationConsult(
        [FromQuery] PaginationConsultsQuery paginationConsultsQuery)
    {
        paginationConsultsQuery.Status = ConsultStatus.Active;
        var paginationConsult = await _mediator.Send(paginationConsultsQuery);
        return Ok(paginationConsult);
    }
}
