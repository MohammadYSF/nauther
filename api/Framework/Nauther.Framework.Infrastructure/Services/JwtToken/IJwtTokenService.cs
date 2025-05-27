using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Nauther.Framework.Infrastructure.Common.Constants;

namespace Nauther.Framework.Infrastructure.Services.JwtToken
{
    public interface IJwtTokenService
    {
        string GenerateToken(DateTime expireTiem, List<Claim> claims);
    }
}
