using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nauther.Framework.Shared.Constants;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Framework.Infrastructure.Middlewares.GlobalExceptionCache;

public class GlobalExceptionCatchMiddleware(RequestDelegate next, ILogger<GlobalExceptionCatchMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionCatchMiddleware> _logger = logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralMessages.InternalServerError);
            
            var result =  new BaseResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = GeneralMessages.InternalServerError
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            
            var jsonResponse = JsonConvert.SerializeObject(result);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
    
}