using AutoMapper;
using EasyCaching.Core;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Infrastructure.Caching.RedisCache;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Commands.DeleteRole;
using Nauther.Identity.Application.Features.Role.Queries;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Infrastructure.Utilities.Constants;
using Newtonsoft.Json.Linq;

namespace Nauther.Identity.Application.Services.Implementations;

public class RoleService(
    IMapper mapper,
    IRoleRepository roleRepository,
    IRedisCacheService redisCacheService,
    IPermissionRepository permissionRepository,
    IRolePermissionRepository rolePermissionRepository,
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository,
    IRedisCachingProvider easyCachingProvider)
    : IRoleService
{
    private readonly IRedisCachingProvider _easyCachingProvider = easyCachingProvider;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;


    public async Task<BaseResponse<IList<GetRolesQueryResponse>?>> GetRolesList(string search, PaginationListDto paginationListDto,
        CancellationToken cancellationToken)
    {
        var total = await _roleRepository.GetCountAsync(search,cancellationToken);
        var roles = await _roleRepository.GetAllListAsync(search, paginationListDto, cancellationToken);
        if (roles == null || roles.Any() == false)
            return new BaseResponse<IList<GetRolesQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.RoleNotFound
            };

        //await _redisCacheService.AddListAsync(CacheKeys.RolesList, roles);
        List<GetRolesQueryResponse> data = [];
        foreach (var role in roles)
        {
            var rolePermissions = await _rolePermissionRepository.GetRolePermissionsByRoleIdAsync(role.Id, cancellationToken);
            var permissionIds = rolePermissions.Select(a => a.PermissionId).ToList();
            var permissions = await _permissionRepository.GetByIdsAsync(permissionIds, cancellationToken);
            var userRoles = await _userRoleRepository.GetUserRolesListByRoleIdAsync(role.Id, cancellationToken);
            var users = await _userRepository.GetByIds(userRoles.Select(a => a.UserId).ToList(), cancellationToken);
            List<GetRolesQueryResponse_User> response_users = [];
            foreach (var item in users)
            {
                var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", item.Id);
                var username = JObject.Parse(user_in_cache)["username"]?.ToObject<string>() ?? string.Empty;
                response_users.Add(new GetRolesQueryResponse_User
                {
                    Id = item.Id,
                    Name = username
                });
            }

            data.Add(new GetRolesQueryResponse
            {
                Id = role.Id,
                DisplayName = role.DisplayName,
                Name = role.Name,
                Permissions = permissions.Select(a => new GetRolesQueryResponse_Permission
                {
                    DisplayName = a.DisplayName,
                    Id = a.Id,
                }).ToList(),
                Users = response_users
            });
        }

        return new BaseResponse<IList<GetRolesQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = data,
            Metadata = new Dictionary<string, object>() { { "total", total } }
        };
    }

    public async Task<BaseResponse<GetRolesQueryResponse?>> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
        if (role == null)
            return new BaseResponse<GetRolesQueryResponse?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.RoleNotFound
            };
        var rolePermissions = await _rolePermissionRepository.GetRolePermissionsByRoleIdAsync(role.Id, cancellationToken);
        var permissionIds = rolePermissions.Select(a => a.PermissionId).ToList();
        var permissions = await _permissionRepository.GetByIdsAsync(permissionIds, cancellationToken);
        var userRoles = await _userRoleRepository.GetUserRolesListByRoleIdAsync(role.Id, cancellationToken);
        var users = await _userRepository.GetByIds(userRoles.Select(a => a.UserId).ToList(), cancellationToken);
        List<GetRolesQueryResponse_User> response_users = [];
        foreach (var item in users)
        {
            var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", item.Id);
            var username = JObject.Parse(user_in_cache)["username"]?.ToObject<string>() ?? string.Empty;
            response_users.Add(new GetRolesQueryResponse_User
            {
                Id = item.Id,
                Name = username
            });
        }
        var data = new GetRolesQueryResponse
        {
            Users = response_users,
            Permissions = permissions.Select(a => new GetRolesQueryResponse_Permission
            {
                DisplayName = a.DisplayName,
                Id = a.Id
            }).ToList(),
            Name = role.Name,
            Id = role.Id,
            DisplayName = role.DisplayName
        };

        return new BaseResponse<GetRolesQueryResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };
    }

    public async Task<BaseResponse<IList<GetRolesQueryResponse>?>> GetRoleByName(string name, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetByNameAsync(name, cancellationToken);
        if (roles == null || roles.Any() == false)
            return new BaseResponse<IList<GetRolesQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.RoleNotFound
            };

        return new BaseResponse<IList<GetRolesQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<IList<GetRolesQueryResponse>?>(roles)
        };
    }

    public async Task<BaseResponse<CreateRoleCommandResponse>> AddRole(CreateRoleCommand dto, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.ExistsByNameAsync(dto.Name, cancellationToken);
        if (existingRole)
            return new BaseResponse<CreateRoleCommandResponse>()
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = Messages.RoleAlreadyExisted
            };
        var role = new Role
        {
            Name = dto.Name,
            DisplayName = dto.DisplayName,
            RolePermissions =dto.Permissions.Select(a => new RolePermission()
            {
                PermissionId = a,
            }).ToList()
        };

        await _roleRepository.AddAsync(role, cancellationToken);
        await _roleRepository.SaveChangesAsync();

        return new BaseResponse<CreateRoleCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = Messages.RoleCreated,
            Data = _mapper.Map<CreateRoleCommandResponse>(role)
        };
    }

    public async Task<BaseResponse> DeleteRoles(DeleteRoleCommand dto, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetByIds(dto.Ids, cancellationToken);
        await _roleRepository.RemoveRange(roles, cancellationToken);
        await _roleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Data = new DeleteRoleCommandResponse { Ids = roles.Select(a => a.Id).ToList() }
        };
    }
}