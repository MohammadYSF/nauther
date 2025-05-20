using MediatR;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRolesList;

public class GetRolesListQuery : PaginationListDto, IRequest<BaseResponse<IList<GetRolesQueryResponse>>>
{
}