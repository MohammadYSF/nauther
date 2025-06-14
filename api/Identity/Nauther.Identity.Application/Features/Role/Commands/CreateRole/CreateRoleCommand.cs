using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Role.Commands.CreateRole;

public class CreateRoleCommand : IRequest<BaseResponse<CreateRoleCommandResponse>>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<Guid> Permissions { get; set; } = [];
}