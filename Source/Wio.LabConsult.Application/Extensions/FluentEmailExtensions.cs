using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wio.LabConsult.Application.Models.Email;
using FluentEmail.Core;

namespace Wio.LabConsult.Application.Extensions;

public static class FluentEmailExtensions
{
    public static void AddServiceEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailFluentSettings>(configuration.GetSection(nameof(EmailFluentSettings)));

        var emailSettings = configuration.GetSection(nameof(EmailFluentSettings));

        var fromEmail = emailSettings.GetValue<string>("Email");

        var host = emailSettings.GetValue<string>("Host");

        var port = emailSettings.GetValue<int>("Port");

        services.AddFluentEmail(fromEmail).AddSmtpSender(host, port);
    }
}
