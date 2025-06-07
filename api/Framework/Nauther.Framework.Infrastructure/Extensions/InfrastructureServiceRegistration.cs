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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using StackExchange.Redis;
using Nauther.Framework.Infrastructure.Services.JwtToken;
using MessageReceivedContext = Microsoft.AspNetCore.Authentication.JwtBearer.MessageReceivedContext;

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
                options.Authority = "https://localhost:44310";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
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
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         ValidIssuer = jwtSettings.Issuer,
            //         ValidAudience = jwtSettings.Audience,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            //     };
            //     options.Events = new JwtBearerEvents
            //     {
            //         OnAuthenticationFailed = context =>
            //         {
            //             Console.WriteLine("Authentication failed: " + context.Exception.Message);
            //             Console.WriteLine("Token: " + context.Request.Headers.Authorization);
            //             return Task.CompletedTask;
            //         }
            //     };
            // })
            //    .AddOpenIdConnect("oidc", options =>
            //         {
            //             options.Authority = configuration["OpenIdConnect:Authority"];
            //             options.RequireHttpsMetadata = configuration.GetValue<bool>("OpenIdConnect:RequireHttpsMetadata");
            //             options.ClientId = configuration["OpenIdConnect:ClientId"];
            //             options.ClientSecret = configuration["OpenIdConnect:ClientSecret"];
            //             options.ResponseType = configuration["OpenIdConnect:ResponseType"];
            //
            //             options.Scope.Clear();
            //             foreach (var scope in configuration.GetValue<string[]>("OpenIdConnect:Scopes")??[])
            //             {
            //                 options.Scope.Add(scope);
            //             }
            //
            //             // options.ClaimActions.MapJsonKey(adminConfiguration.TokenValidationClaimRole, adminConfiguration.TokenValidationClaimRole, adminConfiguration.TokenValidationClaimRole);
            //
            //             options.SaveTokens = true;
            //
            //             options.GetClaimsFromUserInfoEndpoint = true;
            //
            //             options.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 NameClaimType = configuration["OpenIdConnect:TokenValidationClaimName"],
            //                 RoleClaimType = configuration["OpenIdConnect:TokenValidationClaimRole"]
            //             };
            //
            //             options.BackchannelHttpHandler = new HttpClientHandler
            //             {
            //                 ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            //             };
            //
            //             options.Events = new OpenIdConnectEvents
            //             {
            //                 OnMessageReceived = context => OnMessageReceived(context, configuration),
            //                 OnRedirectToIdentityProvider = context => OnRedirectToIdentityProvider(context, configuration)
            //             };
            //         });
            // .AddOpenIdConnect("oidc", options =>
            // {
            //     options.CallbackPath = "/swagger/oauth2-redirect.html";
            //     options.Authority = configuration["OpenIdConnect:Authority"];
            //     options.ClientId = configuration["OpenIdConnect:ClientId"];
            //     options.ClientSecret = configuration["OpenIdConnect:ClientSecret"];
            //     options.ResponseType = "code";
            //
            //     options.SaveTokens = true;
            //     options.GetClaimsFromUserInfoEndpoint = true;
            //
            //     options.Scope.Clear();
            //     options.Scope.Add("openid");
            //     options.Scope.Add("profile");
            //     options.Scope.Add("email");
            //     options.Scope.Add("skoruba_identity_admin_api"); // Add your API scope if needed
            //
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         NameClaimType = "name",
            //         RoleClaimType = "role"
            //     };
            //     options.Events.OnAuthenticationFailed += (context) =>
            //     {
            //         Console.WriteLine("Authentication failed: " + context.Exception.Message);
            //         return Task.CompletedTask;
            //     };
            //     // 👇 This disables SSL validation. For development only.
            //     options.BackchannelHttpHandler = new HttpClientHandler
            //     {
            //         ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            //     };
            // });
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddAuthorization();

        return services;
    }
    private static Task OnMessageReceived(Microsoft.AspNetCore.Authentication.OpenIdConnect.MessageReceivedContext context,IConfiguration configuration)
    {
        context.Properties.IsPersistent = true;
        context.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(configuration.GetValue<int>("OpenIdConnect:IdentityAdminCookieExpiresUtcHours")));

        return Task.CompletedTask;
    }
    private static Task OnRedirectToIdentityProvider(RedirectContext context, IConfiguration configuration)
    {
        if (!string.IsNullOrEmpty(configuration["OpenIdConnect:IdentityAdminRedirectUri"]))
        {
            context.ProtocolMessage.RedirectUri = configuration["OpenIdConnect:IdentityAdminRedirectUri"];
        }

        return Task.CompletedTask;
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