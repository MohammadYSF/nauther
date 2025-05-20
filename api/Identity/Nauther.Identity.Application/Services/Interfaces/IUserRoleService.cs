using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IUserRoleService
{
    Task<BaseResponse<IList<CreateUserRoleCommandResponse>>> AddUserRoles(List<CreateUserRoleDto> dto,
        CancellationToken cancellationToken);
}