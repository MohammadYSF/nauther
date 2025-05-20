using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;

namespace Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;

public class CreateGroupPermissionCommand : IRequest<BaseResponse<IList<CreateGroupPermissionCommandResponse>>>
{
    public List<CreateGroupPermissionDto> CreateGroupPermissionDtos { get; set; }
}