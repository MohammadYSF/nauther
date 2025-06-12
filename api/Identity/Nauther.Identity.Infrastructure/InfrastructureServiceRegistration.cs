using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nauther.Framework.Infrastructure.Common.Models;
using Nauther.Identity.Domain.ExternalContract;
using Nauther.Identity.Infrastructure.Models;
using Nauther.Identity.Infrastructure.Services.ExternalUser;
using Nauther.Identity.Infrastructure.Services.JwtToken;
using Nauther.Identity.Infrastructure.Services.OTP;
using Nauther.Identity.Infrastructure.Utilities.PasswordHash;

namespace Nauther.Identity.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IOtpSender, OtpSender>();
        services.AddScoped<IExternalUserDataRepository, ExternalUserDataRepository>();

        return services;
    }
}