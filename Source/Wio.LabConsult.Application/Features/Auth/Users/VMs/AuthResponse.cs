using Wio.LabConsult.Application.Features.Addresses.Vms;

namespace Wio.LabConsult.Application.Features.Auth.Users.VMs;

public class AuthResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? Avatar { get; set; }
    public AddressVm? AddressVm { get; set; }
    public ICollection<string>? Roles { get; set; }
}
