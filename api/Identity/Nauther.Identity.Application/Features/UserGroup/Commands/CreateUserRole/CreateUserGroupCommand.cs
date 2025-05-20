using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

namespace Nauther.Identity.Application.Features.UserGroup.Commands.CreateUserRole;


public class CreateUserGroupCommand : IRequest<BaseResponse<IList<CreateUserGroupCommandResponse>>>
{
    public List<CreateUserGroupDto> CreateUserGroupDtos { get; set; }
}
