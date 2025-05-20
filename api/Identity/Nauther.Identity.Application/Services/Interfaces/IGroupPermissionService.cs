using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Group.Commands.CreateGroup;
using Nauther.Identity.Application.Features.GroupPermission;
using Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IGroupPermissionService
{
    Task<BaseResponse<IList<CreateGroupPermissionCommandResponse>>> AddGroupPermissions(
        List<CreateGroupPermissionDto> dto,
        CancellationToken cancellationToken);
}