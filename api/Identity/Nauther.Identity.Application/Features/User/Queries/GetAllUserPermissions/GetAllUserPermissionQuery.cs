using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Queries.GetAllUserPermissions;

public class GetAllUserPermissionQuery:IRequest<BaseResponse<List<string>>>
{
    [FromRoute]
    public string Username { get; set; }
    
}