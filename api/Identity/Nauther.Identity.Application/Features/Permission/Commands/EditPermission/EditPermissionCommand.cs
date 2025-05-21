using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Permission.Commands.EditPermission;

public class EditPermissionCommand : IRequest<BaseResponse<EditPermissionCommandResponse>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
}