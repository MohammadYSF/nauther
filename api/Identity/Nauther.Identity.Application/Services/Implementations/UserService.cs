using auther.Identity.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
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
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.ExternalContract;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Infrastructure.Services.JwtToken;
using Nauther.Identity.Infrastructure.Utilities;
using Nauther.Identity.Infrastructure.Utilities.PasswordHash;

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
    IPermissionRepository permissionRepository,
    IRoleRepository roleRepository,
    IUserCredentialRepository userCredentialRepository,
    IExternalUserDataRepository externalUserDataRepository,
    IOptions<DefaultSuperAdminConfiguration> options) : IUserService
{
    private readonly IExternalUserDataRepository _externalUserDataRepository = externalUserDataRepository;
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
    private readonly DefaultSuperAdminConfiguration _defaultSuperAdminConfiguration = options.Value;

    public async Task<BaseResponse> GetExternalUsersList(GetExternalUsersListQuery query,
        CancellationToken cancellationToken)
    {
        string search = query.Search ?? "";
        var res = await _externalUserDataRepository.GetUsersAsync();
        var filtered = res
            .Where(obj =>
                (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
                ||
                (
                    (obj.UserCode.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))
                    ||
                    (obj.PhoneNumber.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))
                    ||
                    (obj.Username.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))
                )
            );

        if (query.PageNumber > -1)
        {
            filtered=filtered.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();
        }


        var total = res.Count;
        List<GetUsersListQueryResponse> data = [];
        foreach (var item in filtered)
        {
            var id = item.Id;
            var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(id, cancellationToken);
            var userPermissions =
                await _userPermissionRepository.GetUserPermissionsByUserIdAsync(id, cancellationToken);
            var permissions =
                await _permissionRepository.GetByIdsAsync(
                    userPermissions.Select(a => a.PermissionId).Distinct().ToList(), cancellationToken);
            var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(),
                cancellationToken);
            var user_in_cache = await _externalUserDataRepository.GetUserByIdentifierAsync(id);
            data.Add(new GetUsersListQueryResponse
            {
                Id = id,
                IsActive = user_in_cache.IsActive,
                Username = user_in_cache.Username,
                PhoneNumber = user_in_cache.PhoneNumber,
                UserCode = user_in_cache.UserCode,
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

    public async Task<BaseResponse<GetUserDetailQueryResponse?>> GetUserDetails(string id, string? username,
        string? phoneNumber,
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
        var userPermissions =
            await _userPermissionRepository.GetUserPermissionsByUserIdAsync(user.Id, cancellationToken);
        var permissions =
            await _permissionRepository.GetByIdsAsync(userPermissions.Select(a => a.PermissionId).Distinct().ToList(),
                cancellationToken);
        var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(),
            cancellationToken);
        var user_in_cache = await _externalUserDataRepository.GetUserByIdentifierAsync(id);
        var userCredentail = await _userCredentialRepository.GetByUserIdAsync(user.Id, cancellationToken);

        var data = new GetUserDetailQueryResponse
        {
            Id = id,
            IsActive = user_in_cache.IsActive,
            Username = user_in_cache.Username,
            PhoneNumber = user_in_cache.PhoneNumber,
            UserCode = user_in_cache.UserCode,
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
        var user_in_cache = await _externalUserDataRepository.GetUserByIdentifierAsync(request.Id);

        if (user_in_cache == null)
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

        var userPermissions =
            await _userPermissionRepository.GetUserPermissionsByUserIdAsync(request.Id, cancellationToken);
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
        var user_in_cache = await _externalUserDataRepository.GetUserByIdentifierAsync(request.Id.ToString());
        if (user_in_cache == null)
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
            }).ToList()
        };
        var userCredential = new UserCredential
        {
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            UserId = request.Id.ToString()
        };
        _ = await _userRepository.AddAsync(u, cancellationToken);
        _ = await _userCredentialRepository.AddAsync(userCredential, cancellationToken);
        await _userRepository.SaveChangesAsync();
        return new BaseResponse<RegisterUserCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Data = new RegisterUserCommandResponse { UserId = request.Id }
        };
    }

    public async Task<BaseResponse<SendOtpCommandResponse>> SendOtp(SendOtpCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<VerifyOtpCommandResponse>> VerifyOtp(VerifyOtpCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<string>>> GetAllPermissionsByUsername(string username,
        CancellationToken cancellationToken)
    {
        var id = string.Empty;
        if (_defaultSuperAdminConfiguration.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
        {
            id = _defaultSuperAdminConfiguration.Id;
        }
        else
        {
            var users_in_cache = await _externalUserDataRepository.GetUsersAsync();
            id = users_in_cache.FirstOrDefault(a => a.UserCode == username)?.Id;
        }

        var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(id, cancellationToken);
        var userPermissions = await _userPermissionRepository.GetUserPermissionsByUserIdAsync(id, cancellationToken);
        var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(),
            cancellationToken);
        var rolePermissions =
            await _rolePermissionRepository.GetRolePermissionsByRoleIdsAsync(
                userRoles.Select(a => a.RoleId).Distinct().ToList(), cancellationToken);

        var permissions = await _permissionRepository.GetByIdsAsync(userPermissions.Select(a => a.PermissionId)
            .Concat(rolePermissions.Select(a => a.PermissionId)).Distinct().ToList(), cancellationToken);


        return new BaseResponse<List<string>>() { Data = permissions.Select(a => a.Name).ToList(), StatusCode = 200 };
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
        var res = await _externalUserDataRepository.GetUsersAsync();
        var filtered = res
            .Where(obj =>
                (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
                ||
                (
                    (obj.UserCode.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ||
                    (obj.PhoneNumber.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ||
                    (obj.Username.Contains(search, StringComparison.OrdinalIgnoreCase))
                )
            );
        if (query.PageNumber > -1)
        {
            filtered=filtered.Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();
        }

        var total = res.Count;
        var users = await _userRepository.GetAllListAsync(new PaginationListDto { PageSize = -1 }, cancellationToken);
        List<GetUsersListQueryResponse> data = [];
        foreach (var item in filtered.Where(a => users.Select(b => b.Id.ToString()).Contains(a.Id)))
        {
            var id = item.Id;
            var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(id, cancellationToken);
            var userPermissions =
                await _userPermissionRepository.GetUserPermissionsByUserIdAsync(id, cancellationToken);
            var permissions =
                await _permissionRepository.GetByIdsAsync(
                    userPermissions.Select(a => a.PermissionId).Distinct().ToList(), cancellationToken);
            var roles = await _roleRepository.GetByIds(userRoles.Select(a => a.RoleId).Distinct().ToList(),
                cancellationToken);
            var user_in_cache = await _externalUserDataRepository.GetUserByIdentifierAsync(id);
            data.Add(new GetUsersListQueryResponse
            {
                Id = id,
                IsActive = user_in_cache.IsActive,
                Username = user_in_cache.Username,
                PhoneNumber = user_in_cache.PhoneNumber,
                UserCode = user_in_cache.UserCode,
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

    public async Task<BaseResponse<CheckPasswordResponse>> CheckPassword(CheckPasswordCommand request,
        CancellationToken cancellationToken)
    {
        User user = null;
        string stored_password_hash = string.Empty;
        if (_defaultSuperAdminConfiguration.Username == request.Username)
        {
            user = _defaultSuperAdminConfiguration.User;
            stored_password_hash = _passwordHasher.HashPassword(_defaultSuperAdminConfiguration.Password);
        }
        else
        {
            var res = await _externalUserDataRepository.GetUsersAsync();

            var filtered = res
                .FirstOrDefault(obj => obj.UserCode == request.Username);
            if (filtered == null)
            {
                return new BaseResponse<CheckPasswordResponse>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = Messages.InvalidUsername
                };
            }

            user = await _userRepository.GetById(filtered.Id, cancellationToken);
            var userCredential = await _userCredentialRepository.GetByUserIdAsync(user.Id, cancellationToken);
            stored_password_hash = userCredential.PasswordHash;
        }

        bool f = _passwordHasher.VerifyPassword(request.Password, stored_password_hash);
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
                Ok = true,
            }
        };
    }
}