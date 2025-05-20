using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.RolePermission.Commands;
using Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;

namespace Nauther.Identity.Application.Services.Implementations;

public class RolePermissionService(
    IMapper mapper,
    IBaseRepository<Role> roleBaseRepository,
    IBaseRepository<Permission> permissionBaseRepository,
    IRolePermissionRepository rolePermissionRepository)
    : IRolePermissionService
{
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
    private readonly IBaseRepository<Role> _roleBaseRepository = roleBaseRepository;
    private readonly IBaseRepository<Permission> _permissionBaseRepository = permissionBaseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<CreateRolePermissionCommandResponse>>> AddRolePermissions(
        List<CreateRolePermissionDto> dtos, CancellationToken cancellationToken)
    {
        var existingRole =
            await _roleBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.RoleId, cancellationToken);
        if (existingRole == null)
            return new BaseResponse<IList<CreateRolePermissionCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.RoleNotFound
            };

        var existingPermission =
            await _permissionBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.PermissionId,
                cancellationToken);
        if (existingPermission == null)
            return new BaseResponse<IList<CreateRolePermissionCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.PermissionNotFound
            };

        var rolePermissions =
            await _rolePermissionRepository.GetRolePermissionsByRoleIdAsync(
                dtos.FirstOrDefault()!.RoleId, cancellationToken);
        await _rolePermissionRepository.RemoveRange(rolePermissions, cancellationToken);
        
        var newRolePermissions = new List<RolePermission>();
        foreach (var item in dtos)
            newRolePermissions.Add(_mapper.Map<RolePermission>(item));

        await _rolePermissionRepository.AddRangeAsync(newRolePermissions, cancellationToken);
        await _rolePermissionRepository.SaveChangesAsync();

        return new BaseResponse<IList<CreateRolePermissionCommandResponse>>()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = Messages.RolePermissionCreated,
            Data = _mapper.Map<IList<CreateRolePermissionCommandResponse>>(newRolePermissions)
        };
    }
}