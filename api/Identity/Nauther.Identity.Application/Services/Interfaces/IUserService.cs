using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.Auth.Commands.SendOtp;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
using Nauther.Identity.Application.Features.User.Commands.CheckPassword;
using Nauther.Identity.Application.Features.User.Commands.DeleteUser;
using Nauther.Identity.Application.Features.User.Commands.EditUser;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;
using Newtonsoft.Json.Linq;

namespace auther.Identity.Application.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponse<CheckPasswordResponse>> CheckPassword(CheckPasswordCommand request,
CancellationToken cancellationToken);

    Task<BaseResponse> Delete(DeleteUserCommand request,
CancellationToken cancellationToken);

    Task<BaseResponse> Edit(EditUserCommand request,
 CancellationToken cancellationToken);
    Task<BaseResponse> GetUsersList(GetUsersListQuery query, CancellationToken cancellationToken);
    Task<BaseResponse> GetExternalUsersList(GetExternalUsersListQuery query, CancellationToken cancellationToken);

    Task<BaseResponse> GetUserDetails(string id, string? username, string? phoneNumber,
        CancellationToken cancellationToken);
    Task<BaseResponse<VerifyNationalCodeCommandResponse?>> GetUserByNationalCode(string nationalCode, CancellationToken cancellationToken);
    Task<BaseResponse> Register(Dima_RegisterUserCommand request, CancellationToken cancellationToken);
    Task<BaseResponse<SendOtpCommandResponse>> SendOtp(SendOtpCommand request, CancellationToken cancellationToken);
    Task<BaseResponse<VerifyOtpCommandResponse>> VerifyOtp(VerifyOtpCommand request, CancellationToken cancellationToken);
    
    Task<BaseResponse<List<string>>> GetAllPermissionsByUsername(string username ,CancellationToken cancellationToken);
}