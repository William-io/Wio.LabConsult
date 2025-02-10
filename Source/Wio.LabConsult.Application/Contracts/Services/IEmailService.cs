using Wio.LabConsult.Application.Models.Email;

namespace Wio.LabConsult.Application.Contracts.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailMessage email, string token);
}
