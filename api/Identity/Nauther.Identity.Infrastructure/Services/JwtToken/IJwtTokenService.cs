using System.Security.Claims;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Infrastructure.Services.JwtToken;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<string> permissions);
}