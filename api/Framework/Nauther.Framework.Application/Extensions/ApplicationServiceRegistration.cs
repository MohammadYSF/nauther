using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nauther.Framework.Application.Services.RequestValidatorService;

namespace Nauther.Framework.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddFluentValidationService(this IServiceCollection services)
    {
        services.AddScoped<IRequestValidator, RequestValidator>();
        
        return services;
    }
}