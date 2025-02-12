using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.LoginUser;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.RegisterUser;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Application.Models.ImageManagement;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public UserController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
    {
        return await _mediator.Send(request);
        
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Register([FromForm] RegisterUserCommand request)
    {
        if(request.Photo is not null)
        {
            var resultImage = await _manageImageService.UploadImage(new ImageData
            {
                ImageStream = request.Photo!.OpenReadStream(),
                Name = request.Photo.Name
            });

            request.PhotoId = resultImage.PublicId;
            request.PhotoUrl = resultImage.Url;
        }

        return await _mediator.Send(request);
    }
}
