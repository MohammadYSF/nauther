using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommand, BaseResponse>
    {
        private readonly IRoleService _roleService = roleService;
        public async Task<BaseResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.DeleteRoles(request, cancellationToken);
        }
    }
}
