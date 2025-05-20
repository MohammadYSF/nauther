using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRoleById;

public class GetRoleByIdQuery : IRequest<BaseResponse<GetRolesQueryResponse>>
{
    public Guid Id { get; set; }
}