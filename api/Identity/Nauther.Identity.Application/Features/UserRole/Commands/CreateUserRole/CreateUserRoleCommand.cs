using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;


public class CreateUserRoleCommand : IRequest<BaseResponse<IList<CreateUserRoleCommandResponse>>>
{
    public List<CreateUserRoleDto> CreateUserRoleDtos { get; set; }
}
