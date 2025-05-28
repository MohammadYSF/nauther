using auther.Identity.Application.Services.Interfaces;
using AutoMapper;
using EasyCaching.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.Auth.Commands.SendOtp;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
using Nauther.Identity.Application.Features.User.Commands.CheckPassword;
using Nauther.Identity.Application.Features.User.Commands.DeleteUser;
using Nauther.Identity.Application.Features.User.Commands.EditUser;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Infrastructure.Services.JwtToken;
using Nauther.Identity.Infrastructure.Utilities.PasswordHash;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nauther.Identity.Application.Services.Implementations;

public class UserService(
    IMapper mapper,
    IUserRepository userRepository,
    IUserPermissionRepository userPermissionRepository,
    IUserRoleRepository userRoleRepository,
    IRolePermissionRepository rolePermissionRepository,
    IUserGroupRepository userGroupRepository,
    IGroupPermissionRepository groupPermissionRepository,
    IPasswordHasherService passwordHasherService,
    IOtpService otpService,
    IJwtTokenService jwtTokenService,
    IRedisCachingProvider _easyCachingProvider,
    IPermissionRepository permissionRepository,
    IRoleRepository roleRepository,
    IUserCredentialRepository userCredentialRepository) : IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserPermissionRepository _userPermissionRepository = userPermissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
    private readonly IUserGroupRepository _userGroupRepository = userGroupRepository;
    private readonly IGroupPermissionRepository _groupPermissionRepository = groupPermissionRepository;
    private readonly IPasswordHasherService _passwordHasher = passwordHasherService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IOtpService _otpService = otpService;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserCredentialRepository _userCredentialRepository = userCredentialRepository;
    private static readonly Guid UserRoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E");


    public async Task<BaseResponse> GetExternalUsersList(GetExternalUsersListQuery query,
        CancellationToken cancellationToken)
    {
        string search = query.Search ?? "";

        var res = await _easyCachingProvider.HGetAllAsync("ids:userbasicinform");
        var filtered = res.Values
    .Select(x => JObject.Parse(x))
    .Where(obj =>
    (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
    ||
    (
    (obj["userCode"]?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
    ||
    (obj["phoneNumber"]?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
    ||
    (obj["username"]?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
    )
    )
          .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        var total = res.Count;
        List<GetUsersListQueryResponse> data = [];
        foreach (var item in filtered)
        {
            var id = item["id"]?.ToString();
            var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(id, cancellationToken);
            var userPermissions = await _userPermissionRepository.GetUserPermissionsByUserIdAsync(id, cancellationToken);
            var permissions = await _permissionRepository.GetByIdsAsync(userPermissions.Select(a => a.PermissionId).Distinct().ToList(), cancellationToken);
            var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(), cancellationToken);
            var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", id);
            data.Add(new GetUsersListQueryResponse
            {
                Id = id,
                IsActive = JObject.Parse(user_in_cache)["isActive"]?.ToObject<bool>() ?? false,
                Username = JObject.Parse(user_in_cache)["username"]?.ToObject<string>() ?? string.Empty,
                PhoneNumber = JObject.Parse(user_in_cache)["PhoneNumber"]?.ToObject<string>() ?? string.Empty,
                UserCode = JObject.Parse(user_in_cache)["userCode"]?.ToObject<string>() ?? string.Empty,
                ProfileImage = string.Empty, //todo
                Permissions = permissions.Select(a => new GetUsersListQueryResponse_Permission
                {
                    DisplayName = a.DisplayName,
                    Id = a.Id
                }).ToList(),
                Roles = roles.Select(a => new GetUsersListQueryResponse_Role
                {
                    DisplayName = a.DisplayName,
                    Id = a.Id
                }).ToList()
            });
        }
        return new BaseResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Data = data,
            Metadata = new Dictionary<string, object>() { { "total", total } }
        };
    }

    public async Task<BaseResponse<GetUserDetailQueryResponse?>> GetUserDetails(string id, string? username, string? phoneNumber,
        CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetUserByPropertiesAsync(
            id,
            string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber,
            string.IsNullOrEmpty(username) ? null : username,
            cancellationToken
        );

        if (user == null)
            return new BaseResponse<GetUserDetailQueryResponse?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };

        var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(user.Id, cancellationToken);
        var userPermissions = await _userPermissionRepository.GetUserPermissionsByUserIdAsync(user.Id, cancellationToken);
        var permissions = await _permissionRepository.GetByIdsAsync(userPermissions.Select(a => a.PermissionId).Distinct().ToList(), cancellationToken);
        var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(), cancellationToken);
        var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", user.Id);
        var userCredentail = await _userCredentialRepository.GetByUserIdAsync(user.Id, cancellationToken);

        var data = new GetUserDetailQueryResponse
        {
            Id = user.Id,
            IsActive = JObject.Parse(user_in_cache)["isActive"]?.ToObject<bool>() ?? false,
            Username = JObject.Parse(user_in_cache)["username"]?.ToObject<string>() ?? string.Empty,
            PhoneNumber = JObject.Parse(user_in_cache)["PhoneNumber"]?.ToObject<string>() ?? string.Empty,
            UserCode = JObject.Parse(user_in_cache)["userCode"]?.ToObject<string>() ?? string.Empty,
            ProfileImage = string.Empty, //todo
            Permissions = permissions.Select(a => new GetUsersListQueryResponse_Permission
            {
                DisplayName = a.DisplayName,
                Id = a.Id
            }).ToList(),
            Roles = roles.Select(a => new GetUsersListQueryResponse_Role
            {
                DisplayName = a.DisplayName,
                Id = a.Id
            }).ToList(),
            CreatedBy = userCredentail.CreatedBy,
            CreatedAt = userCredentail.CreatedAt,
            UpdatedAt = userCredentail.UpdatedAt,
            UpdatedBy = userCredentail.UpdatedBy
        };
        return new BaseResponse<GetUserDetailQueryResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };
    }

    public async Task<BaseResponse<VerifyNationalCodeCommandResponse?>> GetUserByNationalCode(string nationalCode,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByNationalCodeAsync(nationalCode, cancellationToken);
        if (user == null)
            return new BaseResponse<VerifyNationalCodeCommandResponse?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };
        return new BaseResponse<VerifyNationalCodeCommandResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = _mapper.Map<VerifyNationalCodeCommandResponse>(user)
        };
    }
    public async Task<BaseResponse> Edit(EditUserCommand request,
    CancellationToken cancellationToken)
    {
        var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", request.Id.ToString());
        if (string.IsNullOrEmpty(user_in_cache))
        {
            return new BaseResponse<BaseResponse>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };
        }
        if (!string.IsNullOrEmpty(request.Password))
        {
            var userCredentail = await _userCredentialRepository.GetByUserIdAsync(request.Id, cancellationToken);
            userCredentail.PasswordHash = _passwordHasher.HashPassword(request.Password);
            await _userCredentialRepository.UpdateAsync(userCredentail, cancellationToken);
        }
        var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(request.Id, cancellationToken);
        foreach (var item in userRoles)
        {
            if (!request.Roles.Contains(item.RoleId))
            {
                await _userRoleRepository.RemoveAsync(item, cancellationToken);
            }
        }
        foreach (var item in request.Roles)
        {
            if (!userRoles.Select(a => a.RoleId).Contains(item))
            {
                await _userRoleRepository.AddAsync(new UserRole
                {
                    UserId = request.Id,
                    RoleId = item
                }, cancellationToken);
            }
        }
        var userPermissions = await _userPermissionRepository.GetUserPermissionsByUserIdAsync(request.Id, cancellationToken);
        foreach (var item in userPermissions)
        {
            if (!request.Permissions.Contains(item.PermissionId))
            {
                await _userPermissionRepository.RemoveAsync(item, cancellationToken);
            }
        }
        foreach (var item in request.Permissions)
        {
            if (!userPermissions.Select(a => a.PermissionId).Contains(item))
            {
                await _userPermissionRepository.AddAsync(new UserPermission
                {
                    UserId = request.Id,
                    PermissionId = item
                }, cancellationToken);
            }
        }
        await _userRepository.SaveChangesAsync();
        return new BaseResponse()
        {
            StatusCode = StatusCodes.Status201Created,
            Data = new { Id = request.Id }
        };
    }
    public async Task<BaseResponse> Register(Dima_RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", request.Id.ToString());
        if (string.IsNullOrEmpty(user_in_cache))
        {
            return new BaseResponse<BaseResponse>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };
        }
        var u = new User
        {
            Id = request.Id.ToString(),
            UserRoles = request.Roles.Select(a => new UserRole
            {
                RoleId = a,
                UserId = request.Id.ToString()
            }).ToList(),
            UserPermissions = request.Permissions.Select(a => new UserPermission
            {
                PermissionId = a,
                UserId = request.Id.ToString()
            }).ToList(),
            UserCredential = new UserCredential
            {
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                UserId = request.Id.ToString()
            }
        };
        _ = await _userRepository.AddAsync(u, cancellationToken);
        await _userRepository.SaveChangesAsync();
        return new BaseResponse<RegisterUserCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Data = new RegisterUserCommandResponse { UserId = request.Id }
        };
    }

    public async Task<BaseResponse<SendOtpCommandResponse>> SendOtp(SendOtpCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<VerifyOtpCommandResponse>> VerifyOtp(VerifyOtpCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> IsUserExist(Guid? id, string? phoneNumber,
        string? username, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByPropertiesAsync(
            null,
            phoneNumber,
            username,
            cancellationToken
        );

        return user != null;
    }


    public async Task<BaseResponse> GetUsersList(GetUsersListQuery query, CancellationToken cancellationToken)
    {
        string search = query.Search ?? "";

        var res = await _easyCachingProvider.HGetAllAsync("ids:userbasicinform");
        var filtered = res.Values
    .Select(x => JObject.Parse(x))
    .Where(obj =>
    (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
    ||
    (
    (obj["userCode"]?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
    ||
    (obj["phoneNumber"]?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
    ||
    (obj["username"]?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
    )
    )
          .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        var total = res.Count;
        var users = await _userRepository.GetAllListAsync(new PaginationListDto { PageSize = -1 }, cancellationToken);
        List<GetUsersListQueryResponse> data = [];
        foreach (var item in filtered.Where(a => users.Select(b => b.Id.ToString()).Contains(a["id"]?.ToString())))
        {
            var id = item["id"]?.ToString();
            var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(id, cancellationToken);
            var userPermissions = await _userPermissionRepository.GetUserPermissionsByUserIdAsync(id, cancellationToken);
            var permissions = await _permissionRepository.GetByIdsAsync(userPermissions.Select(a => a.PermissionId).Distinct().ToList(), cancellationToken);
            var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(), cancellationToken);
            var user_in_cache = await _easyCachingProvider.HGetAsync("ids:userbasicinform", id);
            data.Add(new GetUsersListQueryResponse
            {
                Id = id,
                IsActive = JObject.Parse(user_in_cache)["isActive"]?.ToObject<bool>() ?? false,
                Username = JObject.Parse(user_in_cache)["username"]?.ToObject<string>() ?? string.Empty,
                PhoneNumber = JObject.Parse(user_in_cache)["PhoneNumber"]?.ToObject<string>() ?? string.Empty,
                UserCode = JObject.Parse(user_in_cache)["userCode"]?.ToObject<string>() ?? string.Empty,
                ProfileImage = string.Empty, //todo
                Permissions = permissions.Select(a => new GetUsersListQueryResponse_Permission
                {
                    DisplayName = a.DisplayName,
                    Id = a.Id
                }).ToList(),
                Roles = roles.Select(a => new GetUsersListQueryResponse_Role
                {
                    DisplayName = a.DisplayName,
                    Id = a.Id
                }).ToList()
            });
        }
        return new BaseResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Data = data,
            Metadata = new Dictionary<string, object>() { { "total", total } }

        };
    }

    public async Task<BaseResponse> Delete(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetByIds(request.Ids, cancellationToken);
        await _userRepository.RemoveRange(users, cancellationToken);
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Data = new DeleteUserCommandResponse
            {
                Ids = users.Select(a => a.Id).ToList()
            }
        };
    }

    public async Task<BaseResponse<CheckPasswordResponse>> CheckPassword(CheckPasswordCommand request, CancellationToken cancellationToken)
    {
        var res = await _easyCachingProvider.HGetAllAsync("ids:userbasicinform");
        var filtered = res.Values
    .Select(x => JObject.Parse(x))
    .Where(obj => obj["userCode"]?.ToString() == request.Username)
    .FirstOrDefault();
        if (filtered == null)
        {
            return new BaseResponse<CheckPasswordResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidUsername
            };
        }

        var user = await _userRepository.GetById(filtered["id"]?.ToString(), cancellationToken);
        var userCredential = await _userCredentialRepository.GetByUserIdAsync(user.Id, cancellationToken);
        bool f = _passwordHasher.VerifyPassword(request.Password, userCredential.PasswordHash);
        if (!f)
        {
            return new BaseResponse<CheckPasswordResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidPassword
            };
        }
        return new BaseResponse<CheckPasswordResponse>
        {
            StatusCode = StatusCodes.Status200OK,
            Data = new CheckPasswordResponse
            {
                Id = user.Id,
                Ok = true
            }
        };

    }
}