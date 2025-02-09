using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Shared;

public sealed class Address : Entity
{
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
}