using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Role.Queries.GetRoleById;

public class GetRoleByIdQuery : IRequest<BaseResponse<GetRolesQueryResponse>>
{
    [FromRoute]
    public Guid Id { get; set; }
}