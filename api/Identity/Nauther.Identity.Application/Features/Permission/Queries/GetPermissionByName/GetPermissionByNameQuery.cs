using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Queries.GetPermissionByName;

public class GetPermissionByNameQuery : IRequest<BaseResponse<IList<GetPermissionsQueryResponse>>>
{
    public string Name { get; set; }
}