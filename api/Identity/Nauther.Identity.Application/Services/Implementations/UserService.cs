using AutoMapper;
using EasyCaching.Core;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.Auth.Commands.SendOtp;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
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
    IRedisCachingProvider _easyCachingProvider) : IUserService
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
    private static readonly Guid UserRoleId = Guid.Parse("717D7A4A-D864-4774-8AE0-5010E745D87E");


    public async Task<BaseResponse> GetUsersList(PaginationListDto paginationListDto,
        CancellationToken cancellationToken)
    {
        var allFields = await _easyCachingProvider.HGetAllAsync("ids:userbasicinform");
        var total = allFields.Count;

        var paged_fields = allFields
            .Skip((paginationListDto.PageNumber - 1) * paginationListDto.PageSize)
            .Take(paginationListDto.PageSize)
;
        return new BaseResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Data = paged_fields,
            Metadata = new Dictionary<string, object>() { { "total", total } }
        };
    }

    public async Task<BaseResponse<GetUserDetailQueryResponse?>> GetUserDetails(Guid? id, string? username, string? phoneNumber,
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

        var userDetailVm = _mapper.Map<GetUserDetailQueryResponse>(user);

        return new BaseResponse<GetUserDetailQueryResponse?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = userDetailVm
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
            UserRoles = request.RoleIds.Select(a => new UserRole
            {
                RoleId = a,
                UserId = request.Id.ToString()
            }).ToList(),
            UserPermissions = request.PermissionIds.Select(a => new UserPermission
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

    public async Task<BaseResponse<LoginWithPasswordCommandResponse>> LoginWithPassword(
        LoginWithPasswordCommand request,
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

    private async Task<string> GenerateUserToken(User user, Guid userId, CancellationToken cancellationToken)
    {
        List<string> permissions = new List<string>();

        permissions.AddRange(await _userPermissionRepository.GetUserPermissionsNameAsync(userId, cancellationToken));

        var userRoles = await _userRoleRepository.GetUserRolesListByUserIdAsync(userId, cancellationToken);
        foreach (var userRole in userRoles ?? new List<UserRole>())
            permissions.AddRange(
                await _rolePermissionRepository.GetRolePermissionsNameAsync(userRole.RoleId, cancellationToken));

        var userGroups = await _userGroupRepository.GetUserGroupsListByUserIdAsync(userId, cancellationToken);
        foreach (var userGroup in userGroups ?? new List<UserGroup>())
            permissions.AddRange(
                await _groupPermissionRepository.GetGroupPermissionsNameAsync(userGroup.GroupId, cancellationToken));

        HashSet<string> distinctPermissions = new HashSet<string>(permissions);

        return _jwtTokenService.GenerateToken(user, distinctPermissions);
    }
}