using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserGroup;
using Nauther.Identity.Application.Features.UserGroup.Commands.CreateUserRole;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;

namespace Nauther.Identity.Application.Services.Implementations;

public class UserGroupService(
    IMapper mapper,
    IBaseRepository<User> userBaseRepository,
    IBaseRepository<Group> groupBaseRepository,
    IUserGroupRepository userGroupRepository) : IUserGroupService
{
    private readonly IMapper _mapper = mapper;
    private readonly IBaseRepository<User> _userBaseRepository = userBaseRepository;
    private readonly IBaseRepository<Group> _groupBaseRepository = groupBaseRepository;
    private readonly IUserGroupRepository _userGroupRepository = userGroupRepository;

    public async Task<BaseResponse<IList<CreateUserGroupCommandResponse>>> AddUserGroups(List<CreateUserGroupDto> dtos,
        CancellationToken cancellationToken)
    {
        var existingUser =
            await _userBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.UserId, cancellationToken);
        if (existingUser == null)
            return new BaseResponse<IList<CreateUserGroupCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };
        
        var existingGroup =
            await _groupBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.GroupId, cancellationToken);
        if (existingGroup == null)
            return new BaseResponse<IList<CreateUserGroupCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.GroupNotFound
            };

        var userGroups =
            await _userGroupRepository.GetUserGroupsListByUserIdAsync(dtos.FirstOrDefault()!.UserId,
                cancellationToken);
        await _userGroupRepository.RemoveRange(userGroups, cancellationToken);

        var newUserGroups = new List<UserGroup>();
        foreach (var item in dtos)
            newUserGroups.Add(_mapper.Map<UserGroup>(item));

        await _userGroupRepository.AddRangeAsync(newUserGroups, cancellationToken);
        await _userGroupRepository.SaveChangesAsync();

         return new BaseResponse<IList<CreateUserGroupCommandResponse>>()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = Messages.UserGroupCreated,
            Data = _mapper.Map<IList<CreateUserGroupCommandResponse>>(newUserGroups)
        };
    }
}