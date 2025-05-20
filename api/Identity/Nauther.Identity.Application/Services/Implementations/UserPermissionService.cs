using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserPermission.Commands;
using Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;

namespace Nauther.Identity.Application.Services.Implementations;

public class UserPermissionService(
    IMapper mapper,
    IBaseRepository<User> userBaseRepository,
    IBaseRepository<Permission> permissionBaseRepository,
    IUserPermissionRepository userPermissionRepository)
    : IUserPermissionService
{
    private readonly IUserPermissionRepository _userPermissionRepository = userPermissionRepository;
    private readonly IBaseRepository<User> _userBaseRepository = userBaseRepository;
    private readonly IBaseRepository<Permission> _permissionBaseRepository = permissionBaseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<IList<CreateUserPermissionCommandResponse>>> AddUserPermissions(
        List<CreateUserPermissionDto> dtos, CancellationToken cancellationToken)
    {
        var existingUser =
            await _userBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.UserId, cancellationToken);
        if (existingUser == null)
            return new BaseResponse<IList<CreateUserPermissionCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };

        var existingPermission =
            await _permissionBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.PermissionId,
                cancellationToken);
        if (existingPermission == null)
            return new BaseResponse<IList<CreateUserPermissionCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.PermissionNotFound
            };

        var userPermissions =
            await _userPermissionRepository.GetUserPermissionsByUserIdAsync(
                dtos.FirstOrDefault()!.UserId, cancellationToken);
        await _userPermissionRepository.RemoveRange(userPermissions, cancellationToken);
        
        var newUserPermissions = new List<UserPermission>();
        foreach (var item in dtos)
            newUserPermissions.Add(_mapper.Map<UserPermission>(item));

        await _userPermissionRepository.AddRangeAsync(newUserPermissions, cancellationToken);
        await _userPermissionRepository.SaveChangesAsync();

        return new BaseResponse<IList<CreateUserPermissionCommandResponse>>()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = Messages.UserPermissionCreated,
            Data = _mapper.Map<IList<CreateUserPermissionCommandResponse>>(newUserPermissions)
        };
    }
}