using MediatR;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Queries;

namespace Nauther.Identity.Application.Features.Group.Queries.GetGroupsList;

public class GetGroupsListQuery : PaginationListDto, IRequest<BaseResponse<IList<GetGroupsQueryResponse>>>
{
}