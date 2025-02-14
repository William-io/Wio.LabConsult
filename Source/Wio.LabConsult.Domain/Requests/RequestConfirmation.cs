using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Requests;

public class RequestConfirmation : Entity
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Cpf { get; set; }
}
