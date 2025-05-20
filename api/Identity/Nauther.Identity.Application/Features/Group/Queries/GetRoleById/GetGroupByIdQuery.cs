using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Queries;

namespace Nauther.Identity.Application.Features.Group.Queries.GetRoleById;

public class GetGroupByIdQuery : IRequest<BaseResponse<GetGroupsQueryResponse>>
{
    public Guid Id { get; set; }
}