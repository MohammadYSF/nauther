using Microsoft.AspNetCore.Http;

namespace Nauther.Framework.Infrastructure.Middlewares.CorrelationId;

public class CorrelationIdHandler : DelegatingHandler
{
    #region Dependecy Injection

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Only add X-Correlation-Id for internal requests, not third-party APIs
        if (_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Correlation-Id") == true)
        {
            request.Headers.Add("X-Correlation-Id",
                _httpContextAccessor.HttpContext.Request.Headers["X-Correlation-Id"].ToString());
        }

        return await base.SendAsync(request, cancellationToken);
    }
}