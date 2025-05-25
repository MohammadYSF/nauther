using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;
using Nauther.Identity.Application.Features.Auth.Commands.Register;
using Nauther.Identity.Application.Features.Auth.Commands.SendOtp;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
using Nauther.Identity.Application.Features.User.Queries.GetUserDetail;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;

namespace Nauther.Identity.Application.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponse> GetUsersList(PaginationListDto paginationListDto, CancellationToken cancellationToken);
    Task<BaseResponse> GetExternalUsersList(PaginationListDto paginationListDto, CancellationToken cancellationToken);

    Task<BaseResponse<GetUserDetailQueryResponse?>> GetUserDetails(Guid? id, string? username, string? phoneNumber,
        CancellationToken cancellationToken);
    Task<BaseResponse<VerifyNationalCodeCommandResponse?>> GetUserByNationalCode(string nationalCode, CancellationToken cancellationToken);
    Task<BaseResponse> Register(Dima_RegisterUserCommand request, CancellationToken cancellationToken);
    Task<BaseResponse<SendOtpCommandResponse>> SendOtp(SendOtpCommand request, CancellationToken cancellationToken);
    Task<BaseResponse<VerifyOtpCommandResponse>> VerifyOtp(VerifyOtpCommand request, CancellationToken cancellationToken);
    Task<BaseResponse<LoginWithPasswordCommandResponse>> LoginWithPassword(LoginWithPasswordCommand request, CancellationToken cancellationToken);
}