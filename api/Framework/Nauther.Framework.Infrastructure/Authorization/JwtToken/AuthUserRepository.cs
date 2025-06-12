using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Infrastructure.Common.Constants;
using Nauther.Framework.Infrastructure.Common.DTOs;

namespace Nauther.Framework.Infrastructure.Authorization.JwtToken;

public class AuthUserRepository(IHttpContextAccessor httpContextAccessor) : IAuthUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public async Task<GetUserDto?> GetUserByTokenAsync()
    {
        string sub = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value??string.Empty;
        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault();
        if (authHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) != true)
            return null;

        var token = authHeader["Bearer ".Length..].Trim();
        
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(token))
            return null;

        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

        return new GetUserDto
        {
            UserId = Guid.Parse(claims.GetValueOrDefault(CustomClaimTypes.UserId)),
            Username = claims.GetValueOrDefault(CustomClaimTypes.Username),
            PhoneNumber = claims.GetValueOrDefault(CustomClaimTypes.PhoneNumber),
            Permissions = claims.TryGetValue(CustomClaimTypes.Permissions, out var value)
                ? value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                : new List<string>()
        };
    }
}