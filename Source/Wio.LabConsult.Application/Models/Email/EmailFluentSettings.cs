namespace Wio.LabConsult.Application.Models.Email;

public class EmailFluentSettings
{
    public string Email { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string BaseUrlClient { get; set; } = string.Empty;
}
