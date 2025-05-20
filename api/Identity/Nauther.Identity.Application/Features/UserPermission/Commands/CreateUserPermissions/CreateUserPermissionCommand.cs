using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;

public class CreateUserPermissionCommand : IRequest<BaseResponse<IList<CreateUserPermissionCommandResponse>>>
{
    public List<CreateUserPermissionDto> CreateUserPermissionDtos { get; set; }
}