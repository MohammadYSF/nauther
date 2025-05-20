using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserGroup;
using Nauther.Identity.Application.Features.UserGroup.Commands.CreateUserRole;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IUserGroupService
{
    Task<BaseResponse<IList<CreateUserGroupCommandResponse>>> AddUserGroups(List<CreateUserGroupDto> dto,
        CancellationToken cancellationToken);
}