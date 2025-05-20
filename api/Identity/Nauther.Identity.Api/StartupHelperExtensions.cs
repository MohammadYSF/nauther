using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Nauther.Framework.Infrastructure.Middlewares.CorrelationId;
using Nauther.Framework.Infrastructure.Middlewares.GlobalExceptionCache;
using Nauther.Identity.Infrastructure.Models;

namespace Nauther.Identity.Api;

public static class StartupHelperExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHealthChecks();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", 
                cp => cp.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Nauther.Identity API", 
                Version = "v1",
            });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer {token}' to authenticate"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.Configure<GateWaySettings>(configuration.GetSection("GateWaySettings"));
        

        return services;
    }
    
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
        { 
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Nauther.Identity API");
                options.WithTheme(ScalarTheme.BluePlanet);
            });
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<GlobalExceptionCatchMiddleware>();
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseCors("AllowAll");
        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health");
        return app;
    }
}