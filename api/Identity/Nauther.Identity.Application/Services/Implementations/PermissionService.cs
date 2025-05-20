using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Infrastructure.Caching.RedisCache;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;
using Nauther.Identity.Application.Features.Permission.Queries;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Infrastructure.Utilities.Constants;

namespace Nauther.Identity.Application.Services.Implementations;

public class PermissionService(IMapper mapper, IPermissionRepository permissionRepository, 
    IRedisCacheService redisCacheService)
    : IPermissionService
{
    private readonly IMapper _mapper = mapper;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;
    
    public async Task<BaseResponse<IList<GetPermissionsQueryResponse>?>> GetPermissionsList(PaginationListDto paginationListDto,CancellationToken cancellationToken)
    {
        var permissions =  await _permissionRepository.GetAllListAsync(paginationListDto,cancellationToken);
        if (permissions == null || permissions.Any() == false)
            return new BaseResponse<IList<GetPermissionsQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.PermissionNotFound
            };
        
        return new BaseResponse<IList<GetPermissionsQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<IList<GetPermissionsQueryResponse>?>(permissions)
        };
    }

    public async Task<BaseResponse<GetPermissionsQueryResponse?>> GetPermissionById(Guid id, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(id, cancellationToken);
        if (permission == null)
            return new BaseResponse<GetPermissionsQueryResponse?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.PermissionNotFound
            };

        return new BaseResponse<GetPermissionsQueryResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<GetPermissionsQueryResponse?>(permission)
        };
    }

    public async Task<BaseResponse<IList<GetPermissionsQueryResponse>?>> GetPermissionByName(string name, CancellationToken cancellationToken)
    {
        var permission =  await _permissionRepository.GetByNameAsync(name, cancellationToken);
        if (permission == null || permission.Any() == false)
            return new BaseResponse<IList<GetPermissionsQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.PermissionNotFound,
            };

        return new BaseResponse<IList<GetPermissionsQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<IList<GetPermissionsQueryResponse>?>(permission)
        };
    }

    public async Task<BaseResponse<CreatePermissionCommandResponse>> AddPermission(CreatePermissionCommand dto, CancellationToken cancellationToken)
    {
        var existingPermission = await _permissionRepository.ExistsByNameAsync(dto.Name, cancellationToken);
        if(existingPermission)
            return new BaseResponse<CreatePermissionCommandResponse>()
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = Messages.PermissionAlreadyExisted
            };
        var permission = _mapper.Map<Permission>(dto);
        
        await _permissionRepository.AddAsync(permission, cancellationToken);
        await _permissionRepository.SaveChangesAsync();
        
        return new BaseResponse<CreatePermissionCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = Messages.PermissionCreated,
            Data = _mapper.Map<CreatePermissionCommandResponse>(permission)
        };
    }
}