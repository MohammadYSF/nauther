using AutoMapper;
using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;

public class CreateRolePermissionCommandHandler(IMapper mapper, IRolePermissionService rolePermissionService)
    : IRequestHandler<CreateRolePermissionCommand,
        BaseResponse<IList<CreateRolePermissionCommandResponse>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IRolePermissionService _rolePermissionService = rolePermissionService;

    public async Task<BaseResponse<IList<CreateRolePermissionCommandResponse>>> Handle(CreateRolePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var rolePermissions =
            await _rolePermissionService.AddRolePermissions(request.CreateRolePermissionDtos, cancellationToken);
        return new BaseResponse<IList<CreateRolePermissionCommandResponse>>()
        {
            StatusCode = rolePermissions.StatusCode,
            Message = rolePermissions.Message,
            Data = rolePermissions.Data
        };
    }
}