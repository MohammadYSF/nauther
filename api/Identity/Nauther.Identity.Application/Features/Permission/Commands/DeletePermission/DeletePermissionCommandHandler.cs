using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Permission.Commands.De_etePermission
{
    public class DeletePermissionCommandHandler(IPermissionService _permissionService) : IRequestHandler<DeletePermissionCommand, BaseResponse>
    {
        public async Task<BaseResponse> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var res = await _permissionService.DeletePermission(request, cancellationToken);
            return res;
        }
    }
}
