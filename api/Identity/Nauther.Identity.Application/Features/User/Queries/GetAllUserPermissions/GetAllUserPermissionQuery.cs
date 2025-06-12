using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Queries.GetAllUserPermissions;

public class GetAllUserPermissionQuery:IRequest<BaseResponse<List<string>>>
{
    public string Id { get; set; }
    
}