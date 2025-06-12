using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Role.Commands.EditRole;

public class EditRoleCommandHandler(IRoleService roleService):IRequestHandler<EditRoleCommand,BaseResponse<EditRoleCommandResponse>>
{
    private readonly IRoleService _roleService = roleService;
    public async Task<BaseResponse<EditRoleCommandResponse>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.EditRole(request,cancellationToken);
    }
}