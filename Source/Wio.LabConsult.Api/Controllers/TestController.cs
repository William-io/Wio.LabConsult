
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Models.Email;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    private readonly IEmailService _emailService;

    public TestController(IEmailService emailService)
    {
        _emailService = emailService;
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> SendEmail()
    {
        var message = new EmailMessage
        {
            To = "vaxi.drez@gmail.com",
            Body = "Esta es una prueba de envio de email con token",
            Subject = "Cambiar el Password",
        };

        var result = await _emailService.SendEmailAsync(message, "Este_Es_Mi_Token");

        return result ? Ok() : BadRequest();
    }

}
