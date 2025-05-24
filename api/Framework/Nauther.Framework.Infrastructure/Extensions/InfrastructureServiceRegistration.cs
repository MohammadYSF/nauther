using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Nauther.Framework.Infrastructure.Authorization.JwtToken;
using Nauther.Framework.Infrastructure.Caching.RedisCache;
using Nauther.Framework.Infrastructure.Common.Constants;
using Nauther.Framework.Infrastructure.Common.Models;
using Nauther.Framework.Infrastructure.Middlewares.CorrelationId;
using Nauther.Framework.Infrastructure.Services.FileService;
using EasyCaching.Core.Configurations;
using StackExchange.Redis;

namespace Nauther.Framework.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddHttpClientHandlers(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<CorrelationIdHandler>();
        services.AddHttpClient(HttpClientNames.InternalService).AddHttpMessageHandler<CorrelationIdHandler>();
        services.AddHttpClient(HttpClientNames.ThirdPartyService);
        return services;
    }

    public static void AddConfiguration(this IConfigurationBuilder configurationBuilder, IHostEnvironment environment)
    {
        configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        if (environment.EnvironmentName == "Local")
        {
            configurationBuilder.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
        }
    }

    public static IServiceCollection AddAuthUserService(this IServiceCollection services)
    {
        services.AddScoped<IAuthUserService, AuthUserRepository>();

        return services;
    }
    public static IServiceCollection AddEasyCachingService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RedisConnectionString");
        var configurationOptions = ConfigurationOptions.Parse(connectionString);

        return services.AddEasyCaching(options =>
        {
            options.UseRedis(config =>
            {
                config.DBConfig.ConfigurationOptions = configurationOptions;
                config.DBConfig.Database = 0;
                config.SerializerName = "DefaultRedis"; // this demands a serializer named "DefaultRedis"

            });
            options.WithJson("DefaultRedis"); // register Json serializer under that name

        });
    }
    public static IServiceCollection AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RedisConnectionString");
        services.AddSingleton<IRedisCacheService>(new RedisCacheService(connectionString));

        return services;
    }

    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        Console.WriteLine("Token: " + context.Request.Headers.Authorization);
                        return Task.CompletedTask;
                    }
                };
            });
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddInquiryServices(this IServiceCollection services,
        IConfiguration configuration)
    {


        return services;
    }

    public static IServiceCollection AddFileService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileSettings>(configuration.GetSection("FileSettings"));

        services.AddScoped<IFileService, Services.FileService.FileService>();

        return services;
    }
}