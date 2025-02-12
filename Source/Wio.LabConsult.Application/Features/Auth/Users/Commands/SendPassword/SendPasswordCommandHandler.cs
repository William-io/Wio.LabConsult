using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using System.Text;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Models.Email;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.SendPassword;

public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
{
    private readonly IEmailService _serviceEmail;
    private readonly UserManager<User> _userManager;

    public SendPasswordCommandHandler(IEmailService serviceEmail, UserManager<User> userManager)
    {
        _serviceEmail = serviceEmail;
        _userManager = userManager;
    }

    public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new BadRequestException("Usuario não existe!");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var plainTextBytes = Encoding.UTF8.GetBytes(token);
        token = Convert.ToBase64String(plainTextBytes);

        var emailMessage = new EmailMessage
        {
            To = request.Email,
            Body = "Recuperação de senha",
            Subject = "Alterar a senha"
        };

        var result = await _serviceEmail.SendEmailAsync(emailMessage, token);

        if (!result)
        {
            throw new Exception("Erro ao enviar email");
        }

        return $"O e-mail foi enviado para a conta {request.Email}";
    }
}
