using AutoMapper;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;

namespace Nauther.Identity.Application.Services.Implementations;

public class UserRoleService(
    IMapper mapper,
    IBaseRepository<User> userBaseRepository,
    IBaseRepository<Role> roleBaseRepository,
    IUserRoleRepository userRoleRepository) : IUserRoleService
{
    private readonly IMapper _mapper = mapper;
    private readonly IBaseRepository<User> _userBaseRepository = userBaseRepository;
    private readonly IBaseRepository<Role> _roleBaseRepository = roleBaseRepository;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;

    public async Task<BaseResponse<IList<CreateUserRoleCommandResponse>>> AddUserRoles(List<CreateUserRoleDto> dtos,
        CancellationToken cancellationToken)
    {
        var existingUser =
            await _userBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.UserId, cancellationToken);
        if (existingUser == null)
            return new BaseResponse<IList<CreateUserRoleCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };
        
        var existingRole =
            await _roleBaseRepository.GetByIdAsync(dtos.FirstOrDefault()!.RoleId, cancellationToken);
        if (existingRole == null)
            return new BaseResponse<IList<CreateUserRoleCommandResponse>>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.RoleNotFound
            };

        var userRoles =
            await _userRoleRepository.GetUserRolesListByUserIdAsync(dtos.FirstOrDefault()!.UserId,
                cancellationToken);
        await _userRoleRepository.RemoveRange(userRoles, cancellationToken);
        await _userRoleRepository.SaveChangesAsync();

        var newUserRoles = new List<UserRole>();
        foreach (var item in dtos)
            newUserRoles.Add(_mapper.Map<UserRole>(item));

        await _userRoleRepository.AddRangeAsync(newUserRoles, cancellationToken);
        await _userRoleRepository.SaveChangesAsync();

         return new BaseResponse<IList<CreateUserRoleCommandResponse>>()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = Messages.UserRoleCreated,
            Data = _mapper.Map<IList<CreateUserRoleCommandResponse>>(newUserRoles)
        };
    }
}