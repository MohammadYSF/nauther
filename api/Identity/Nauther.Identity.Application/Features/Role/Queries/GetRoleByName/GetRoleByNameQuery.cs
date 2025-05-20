using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRoleByName;

public class GetRoleByNameQuery : IRequest<BaseResponse<IList<GetRolesQueryResponse>>>
{
    public string Name { get; set; }
}