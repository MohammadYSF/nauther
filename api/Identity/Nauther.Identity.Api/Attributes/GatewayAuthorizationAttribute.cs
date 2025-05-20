using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Nauther.Identity.Infrastructure.Models;

namespace Nauther.Identity.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class GatewayAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var gateWaySettings = context.HttpContext.RequestServices
            .GetRequiredService<IOptionsMonitor<GateWaySettings>>().CurrentValue;

        if (gateWaySettings.XApiKey == null)
        {
            throw new ArgumentNullException("Gateway settings not found");
        }

        var requestHeader = context.HttpContext.Request.Headers;
        if (requestHeader.TryGetValue("X-Api-Key", out var requestApiKey) == false || requestApiKey != gateWaySettings.XApiKey)
        {
            context.Result = new ObjectResult(new
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Invalid API Key"
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}