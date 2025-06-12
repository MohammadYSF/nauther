using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;

namespace Nauther.Identity.Application.Features.Role.Commands.EditRole;

public class EditRoleCommand: IRequest<BaseResponse<EditRoleCommandResponse>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<Guid> Permissions { get; set; } = [];
}