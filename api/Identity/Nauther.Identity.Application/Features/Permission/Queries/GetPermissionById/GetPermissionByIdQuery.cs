using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Queries.GetPermissionById;

public class GetPermissionByIdQuery : IRequest<BaseResponse<GetPermissionsQueryResponse>>
{
    public Guid Id { get; set; }
}