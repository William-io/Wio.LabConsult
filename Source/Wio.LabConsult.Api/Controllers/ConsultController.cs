using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Net;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;
using Wio.LabConsult.Application.Features.Consults.Commands.DeleteConsult;
using Wio.LabConsult.Application.Features.Consults.Commands.UpdateConsult;
using Wio.LabConsult.Application.Features.Consults.Queries.GetConsultById;
using Wio.LabConsult.Application.Features.Consults.Queries.GetConsultList;
using Wio.LabConsult.Application.Features.Consults.Queries.PaginationConsults;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Models.Authorization;
using Wio.LabConsult.Application.Models.ImageManagement;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ConsultController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public ConsultController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    // GET: api/v1/Consult
    [AllowAnonymous]
    [HttpGet("list", Name = "GetConsultList")]
    [ProducesResponseType(typeof(IReadOnlyList<ConsultVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ConsultVm>>> GetConsultList()
    {
        var query = new GetConsultListQuery();
        var consults = await _mediator.Send(query);  
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

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetConsultById")]
    [ProducesResponseType(typeof(ConsultVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ConsultVm>> GetConsultById(int id)
    {
        var query = new GetConsultByIdQuery(id);
        return Ok(await _mediator.Send(query));
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPost("create", Name = "CreateConsult")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ConsultVm>> CreateConsult([FromForm] CreateConsultCommand request)
    {
        var listPhotoUrls = new List<CreateConsultImageCommand>();

        if(request.Photos is not null)
        {
            foreach(var Photo in request.Photos)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                { 
                    ImageStream = Photo.OpenReadStream(),
                    Name = Photo.Name
                });

                var photoCommand = new CreateConsultImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                listPhotoUrls.Add(photoCommand);
            }

            request.ImageUrls = listPhotoUrls;
        }

        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("update", Name = "UpdateConsult")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ConsultVm>> UpdateConsult([FromForm] UpdateConsultCommand request)
    {
        var listFotoUrls = new List<CreateConsultImageCommand>();

        if (request.Photos is not null)
        {
            foreach (var foto in request.Photos)
            {
                var resultImage = await _manageImageService.UploadImage(new ImageData
                {
                    ImageStream = foto.OpenReadStream(),
                    Name = foto.Name
                });

                var fotoCommand = new CreateConsultImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                listFotoUrls.Add(fotoCommand);
            }
            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);

    }

    //Não vai deletar apenas atualizar o status de uma consulta ativa para inativa
    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("status/{id}", Name = "UpdateStatusConsult")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ConsultVm>> UpdateStatusConsult(int id)
    {
        var request = new DeleteConsultCommand(id);
        return await _mediator.Send(request);
    }
}
