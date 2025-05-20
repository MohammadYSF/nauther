using AutoMapper;
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
    IJwtTokenService jwtTokenService) : IUserService
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

    public async Task<BaseResponse<IList<GetUsersListQueryResponse>?>> GetUsersList(PaginationListDto paginationListDto,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllListAsync(paginationListDto, cancellationToken);
        if (users == null || users.Any() == false)
            return new BaseResponse<IList<GetUsersListQueryResponse>?>()
            {
                StatusCode = StatusCodes.Status203NonAuthoritative,
                Message = Messages.UserNotFound
            };
        var usersVm = _mapper.Map<IList<GetUsersListQueryResponse>>(users);

        return new BaseResponse<IList<GetUsersListQueryResponse>?>()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = usersVm
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

    public async Task<BaseResponse<RegisterUserCommandResponse>> Register(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await IsUserExist(null, request.PhoneNumber,
            request.NationalCode, cancellationToken);
        if (existingUser)
            return new BaseResponse<RegisterUserCommandResponse>()
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = Messages.UserAlreadyExisted
            };

    

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.UserCredential = new UserCredential()
        {
            UserId = user.Id,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync();
        
        await _otpService.SendOtpAsync(user.PhoneNumber);

        return new BaseResponse<RegisterUserCommandResponse>()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = Messages.UserRegistered,
            Data = _mapper.Map<RegisterUserCommandResponse>(user)
        };
    }

    public async Task<BaseResponse<SendOtpCommandResponse>> SendOtp(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByPropertiesAsync(
            request.UserId,
            null,
            null,
            cancellationToken
        );
        if (user == null)
            return new BaseResponse<SendOtpCommandResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidUsername
            };

        await _otpService.SendOtpAsync(user.PhoneNumber);
        
        return new BaseResponse<SendOtpCommandResponse>()
        {
            StatusCode = StatusCodes.Status200OK,
        };
    }

    public async Task<BaseResponse<VerifyOtpCommandResponse>> VerifyOtp(VerifyOtpCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByPropertiesAsync(
            request.UserId,
            null,
            null,
            cancellationToken
        );
        if (user == null)
            return new BaseResponse<VerifyOtpCommandResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidUsername
            };

        var isOtpVerified = await _otpService.VerifyOtpAsync(user.PhoneNumber, request.Otp);
        if (isOtpVerified == false)
            return new BaseResponse<VerifyOtpCommandResponse>()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidOtp
            };

        if (request.OtpType == OtpType.RegistrationOtp)
        {
            user.IsActive = true;
            user.UserRoles.Add(new UserRole()
            {
                UserId = user.Id,
                RoleId = UserRoleId
            });
            await _userRoleRepository.AddRangeAsync(user.UserRoles.ToList(), cancellationToken);
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync();
        }

        if (request.OtpType == OtpType.LoginOtp)
        {
            var token = await GenerateUserToken(user, user.Id, cancellationToken);
            return new BaseResponse<VerifyOtpCommandResponse>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = Messages.LoginSuccessfully,
                Data = _mapper.Map<VerifyOtpCommandResponse>(token)
            };
        }
        
        return new BaseResponse<VerifyOtpCommandResponse>()
        {
            StatusCode = StatusCodes.Status200OK,
        };
    }

    public async Task<BaseResponse<LoginWithPasswordCommandResponse>> LoginWithPassword(
        LoginWithPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByPropertiesAsync(
            request.UserId,
            null,
            null,
            cancellationToken
        );
        if (user == null)
            return new BaseResponse<LoginWithPasswordCommandResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidUsername
            };

        var passwordVerification = _passwordHasher.VerifyPassword(request.Password, user.UserCredential.PasswordHash);
        if (passwordVerification == false)
            return new BaseResponse<LoginWithPasswordCommandResponse>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = Messages.InvalidPassword
            };

        var token = await GenerateUserToken(user, user.Id, cancellationToken);
        return new BaseResponse<LoginWithPasswordCommandResponse>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = Messages.LoginSuccessfully,
            Data = _mapper.Map<LoginWithPasswordCommandResponse>(token)
        };
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