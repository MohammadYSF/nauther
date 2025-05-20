using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserPermission.Commands;
using Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IUserPermissionService
{
    Task<BaseResponse<IList<CreateUserPermissionCommandResponse>>> AddUserPermissions(List<CreateUserPermissionDto> dto,
        CancellationToken cancellationToken);
}