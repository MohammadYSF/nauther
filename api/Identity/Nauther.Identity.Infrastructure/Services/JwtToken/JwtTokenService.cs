using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nauther.Framework.Infrastructure.Common.Constants;
using Nauther.Framework.Infrastructure.Common.Models;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Infrastructure.Models;

namespace Nauther.Identity.Infrastructure.Services.JwtToken;

public class JwtTokenService(IOptionsMonitor<JwtSettings> jwtSettings) : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.CurrentValue;

    public string GenerateToken(User user, IEnumerable<string> permissions)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
            new Claim(CustomClaimTypes.NationalCode, user.NationalCode),
            new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber),
            new Claim(CustomClaimTypes.IsActive, user.IsActive.ToString())
        };
        claims.Add(new Claim(CustomClaimTypes.Permissions, string.Join(",", permissions)));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = credentials,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}