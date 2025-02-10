using FluentEmail.Core;
using Microsoft.Extensions.Options;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Models.Email;

namespace Wio.LabConsult.Infrastructure.MessageImplementation;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly EmailFluentSettings _emailFluentSettings;

    public EmailService(IFluentEmail fluentEmail, IOptions<EmailFluentSettings> emailFluentSettings)
    {
        _fluentEmail = fluentEmail;
        _emailFluentSettings = emailFluentSettings.Value;
    }

    public async Task<bool> SendEmailAsync(EmailMessage email, string token)
    {
        var htmlContent = $"{email.Body} {_emailFluentSettings.BaseUrlClient}/password/reset/{token}";

        var result = await _fluentEmail
        .To(email.To)
        .Subject(email.Subject)
        .Body(htmlContent)
        .SendAsync();

        return result.Successful;

    }
}
