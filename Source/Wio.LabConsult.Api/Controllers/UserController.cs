using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.LoginUser;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.RegisterUser;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.ResetPassword;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.ResetPasswordByToken;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.SendPassword;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateAdminUser;
using Wio.LabConsult.Application.Features.Auth.Users.Commands.UpdateUser;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Application.Models.ImageManagement;
using Wio.LabConsult.Domain.Users;
using Role = Wio.LabConsult.Application.Models.Authorization.Role;


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

    [AllowAnonymous]
    [HttpPost("forgotpassword", Name = "ForgotPassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("resetpassword", Name = "ResetPassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("updatepassword", Name = "UpdatePassword")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommand request)
    {
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpPost("update", Name = "Update")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Update([FromForm] UpdateUserCommand request)
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

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<User>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
    {
        return await _mediator.Send(request);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("updateAdminStatusUser", Name = "UpdateAdminStatusUser")]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<User>> UpdateAdminStatusUser([FromBody] UpdateAdminUserCommand request)
    {
        return await _mediator.Send(request);
    }
}
