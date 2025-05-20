using MediatR;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Queries.GetPermissionList;

public class GetPermissionsListQuery : PaginationListDto, IRequest<BaseResponse<IList<GetPermissionsQueryResponse>>>
{
    
}