using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Shared;

public class Country : Entity
{
    public string? Name { get; set; }
    public string? Iso2 { get; set; }
    public string? Iso3 { get; set; }
}
