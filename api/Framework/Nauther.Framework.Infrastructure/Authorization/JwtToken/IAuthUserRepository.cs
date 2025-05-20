using Nauther.Framework.Infrastructure.Common.DTOs;

namespace Nauther.Framework.Infrastructure.Authorization.JwtToken;

public interface IAuthUserService
{
    Task<GetUserDto?> GetUserByTokenAsync();
}