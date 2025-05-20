using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.RolePermission.Commands;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IRolePermissionService
{
    Task<BaseResponse<IList<CreateRolePermissionCommandResponse>>> AddRolePermissions(List<CreateRolePermissionDto> dto,
        CancellationToken cancellationToken);
}