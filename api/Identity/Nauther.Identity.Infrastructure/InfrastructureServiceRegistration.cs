using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Nauther.Framework.Infrastructure.Common.Models;
using Nauther.Identity.Domain.ExternalContract;
using Nauther.Identity.Infrastructure.Models;
using Nauther.Identity.Infrastructure.Services.ExternalUser;
using Nauther.Identity.Infrastructure.Services.JwtToken;
using Nauther.Identity.Infrastructure.Services.OTP;
using Nauther.Identity.Infrastructure.Utilities;
using Nauther.Identity.Infrastructure.Utilities.PasswordHash;

namespace Nauther.Identity.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("aied_mongodb");
            return new MongoClient(connectionString);
        });
        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            // Replace "MyDatabase" with your actual database name
            return client.GetDatabase("aied_db");
        });
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IOtpSender, OtpSender>();
        //services.AddScoped<IExternalUserDataRepository, External_Dima_UserDataRepository>();
        services.AddScoped<IExternalUserDataRepository<External_AIED_UserModel>, External_AIED_UserDataRepository>();
        
        services.Configure<DefaultSuperAdminConfiguration>(configuration.GetSection(nameof(DefaultSuperAdminConfiguration)));

        return services;
    }
}