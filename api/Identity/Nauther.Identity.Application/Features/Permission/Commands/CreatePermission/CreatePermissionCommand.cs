using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;

public class CreatePermissionCommand :IRequest<BaseResponse<CreatePermissionCommandResponse>>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
}
