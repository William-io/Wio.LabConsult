using Microsoft.AspNetCore.Identity;

namespace Wio.LabConsult.Domain.Users;

public class User : IdentityUser
{
    public string? Name { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;
}
