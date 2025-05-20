using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Infrastructure.Caching.RedisCache;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Group.Commands.CreateGroup;
using Nauther.Identity.Application.Features.Group.Queries;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Infrastructure.Utilities.Constants;

namespace Nauther.Identity.Application.Services.Implementations;

public class GroupService(
    IMapper mapper,
    IGroupRepository groupRepository,
    IRedisCacheService redisCacheService)
    : IGroupService
{
    private readonly IMapper _mapper = mapper;
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;


    public async Task<BaseResponse<IList<GetGroupsQueryResponse>?>> GetGroupsList(PaginationListDto paginationListDto,
        CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetAllListAsync(paginationListDto, cancellationToken);
        if (groups == null || groups.Any() == false)
            return new BaseResponse<IList<GetGroupsQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.GroupNotFound
            };
        
        return new BaseResponse<IList<GetGroupsQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<IList<GetGroupsQueryResponse>?>(groups)
        };
    }

    public async Task<BaseResponse<GetGroupsQueryResponse?>> GetGroupById(Guid id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);
        if (group == null)
            return new BaseResponse<GetGroupsQueryResponse?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.GroupNotFound
            };

        return new BaseResponse<GetGroupsQueryResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<GetGroupsQueryResponse?>(group)
        };
    }

    public async Task<BaseResponse<IList<GetGroupsQueryResponse>?>> GetGroupByName(string name, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByNameAsync(name, cancellationToken);
        if (groups == null || groups.Any() == false)
            return new BaseResponse<IList<GetGroupsQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.GroupNotFound
            };

        return new BaseResponse<IList<GetGroupsQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<IList<GetGroupsQueryResponse>?>(groups)
        };
    }

    public async Task<BaseResponse<CreateGroupCommandResponse>> AddGroup(CreateGroupCommand dto, CancellationToken cancellationToken)
    {
        var existingGroup = await _groupRepository.ExistsByNameAsync(dto.Name, cancellationToken);
        if (existingGroup)
            return new BaseResponse<CreateGroupCommandResponse>()
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = Messages.GroupAlreadyExisted
            };
        var group = _mapper.Map<Group>(dto);

        await _groupRepository.AddAsync(group, cancellationToken);
        await _groupRepository.SaveChangesAsync();

        return new BaseResponse<CreateGroupCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = Messages.GroupCreated,
            Data = _mapper.Map<CreateGroupCommandResponse>(group)
        };
    }
}