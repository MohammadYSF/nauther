using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Group.Commands.CreateGroup;
using Nauther.Identity.Application.Features.GroupPermission;
using Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;

namespace Nauther.Identity.Application.Services.Implementations;

public class GroupPermissionService(
    IMapper mapper,
    IBaseRepository<Group> groupBaseRepository,
    IBaseRepository<Permission> permissionBaseRepository,
    IGroupPermissionRepository groupPermissionRepository)
    : IGroupPermissionService
{
    private readonly IGroupPermissionRepository _groupPermissionRepository = groupPermissionRepository;
    private readonly IBaseRepository<Group> _groupBaseRepository = groupBaseRepository;
    private readonly IBaseRepository<Permission> _permissionBaseRepository = permissionBaseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<CreateGroupPermissionCommandResponse>>> AddGroupPermissions(
        List<CreateGroupPermissionDto> dtos, CancellationToken cancellationToken)
    {
        var existingGroup =
            await _groupBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.GroupId, cancellationToken);
        if (existingGroup == null)
            return new BaseResponse<IList<CreateGroupPermissionCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.GroupNotFound
            };

        var existingPermission =
            await _permissionBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.PermissionId,
                cancellationToken);
        if (existingPermission == null)
            return new BaseResponse<IList<CreateGroupPermissionCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.PermissionNotFound
            };

        var groupPermissions =
            await _groupPermissionRepository.GetGroupPermissionsByGroupIdAsync(
                dtos.FirstOrDefault()!.GroupId, cancellationToken);
        await _groupPermissionRepository.RemoveRange(groupPermissions, cancellationToken);
        
        var newGroupPermissions = new List<GroupPermission>();
        foreach (var item in dtos)
            newGroupPermissions.Add(_mapper.Map<GroupPermission>(item));

        await _groupPermissionRepository.AddRangeAsync(newGroupPermissions, cancellationToken);
        await _groupPermissionRepository.SaveChangesAsync();

        return new BaseResponse<IList<CreateGroupPermissionCommandResponse>>()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = Messages.GroupPermissionCreated,
            Data = _mapper.Map<IList<CreateGroupPermissionCommandResponse>>(newGroupPermissions)
        };
    }
}