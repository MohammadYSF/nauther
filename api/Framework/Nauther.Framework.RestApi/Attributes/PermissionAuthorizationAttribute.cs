using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nauther.Framework.Shared.Constants;

namespace Nauther.Framework.RestApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class PermissionAuthorizationAttribute(string permission) : Attribute, IAuthorizationFilter
{
    private readonly string _permission = permission;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context?.HttpContext?.User is null)
        {
            return;
        }

        var user = context.HttpContext.User;
        
        if (user.Identity?.IsAuthenticated == false)
        {
            SetUnauthorizedResult(context);
        }

        else if (HasPermission(user, _permission) == false)
        {
            SetForbiddenResult(context);
        }
    }

    private static bool HasPermission(ClaimsPrincipal user, string permission)
    {
        return user.HasClaim(c => c.Value.Contains(permission, StringComparison.OrdinalIgnoreCase));
    }

    private static void SetUnauthorizedResult(AuthorizationFilterContext context)
    {
        context.Result = new ObjectResult(new 
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            Message = GeneralMessages.Unauthorized
        })
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
    private static void SetForbiddenResult(AuthorizationFilterContext context)
    {
        context.Result = new ObjectResult(new 
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = GeneralMessages.Forbidden
        })
        {
            StatusCode = StatusCodes.Status403Forbidden
        };;
    }
    
}