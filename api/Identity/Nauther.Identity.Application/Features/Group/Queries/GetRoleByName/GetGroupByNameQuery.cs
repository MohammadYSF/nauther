using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Queries;

namespace Nauther.Identity.Application.Features.Group.Queries.GetRoleByName;

public class GetGroupByNameQuery : IRequest<BaseResponse<IList<GetGroupsQueryResponse>>>
{
    public string Name { get; set; }
}