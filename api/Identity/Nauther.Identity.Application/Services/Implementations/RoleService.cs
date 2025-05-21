using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Infrastructure.Caching.RedisCache;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Features.Role.Queries;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Infrastructure.Utilities.Constants;

namespace Nauther.Identity.Application.Services.Implementations;

public class RoleService(
    IMapper mapper,
    IRoleRepository roleRepository,
    IRedisCacheService redisCacheService)
    : IRoleService
{
    private readonly IMapper _mapper = mapper;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;


    public async Task<BaseResponse<IList<GetRolesQueryResponse>?>> GetRolesList(PaginationListDto paginationListDto,
        CancellationToken cancellationToken)
    {
        var total = await _roleRepository.GetCountAsync(cancellationToken);
        var roles = await _roleRepository.GetAllListAsync(paginationListDto, cancellationToken);
        if (roles == null || roles.Any() == false)
            return new BaseResponse<IList<GetRolesQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.RoleNotFound
            };

        await _redisCacheService.AddListAsync(CacheKeys.RolesList, roles);

        return new BaseResponse<IList<GetRolesQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<IList<GetRolesQueryResponse>?>(roles),
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

        return new BaseResponse<GetRolesQueryResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<GetRolesQueryResponse?>(role)
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
        var role = _mapper.Map<Role>(dto);

        await _roleRepository.AddAsync(role, cancellationToken);
        await _roleRepository.SaveChangesAsync();

        return new BaseResponse<CreateRoleCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = Messages.RoleCreated,
            Data = _mapper.Map<CreateRoleCommandResponse>(role)
        };
    }
}