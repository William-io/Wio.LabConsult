using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Models.Token;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    public JwtSettings _jwtSettings { get; }
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtSettings = jwtSettings.Value;
    }

    public string CreateToken(User user, IList<string>? roles)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim("userId", user.Id),
            new Claim("email", user.Email!)
        };

        foreach (var rol in roles!)
        {
            var claim = new Claim(ClaimTypes.Role, rol);
            claims.Add(claim);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
            SigningCredentials = credenciales
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    public string GetSessionUser()
    {
        var username = _httpContextAccessor.HttpContext!.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        return username!;
    }
}
