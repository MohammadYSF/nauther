using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;

public class CreateRolePermissionCommand : IRequest<BaseResponse<IList<CreateRolePermissionCommandResponse>>>
{
    public List<CreateRolePermissionDto> CreateRolePermissionDtos { get; set; }
}