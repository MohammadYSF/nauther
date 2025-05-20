using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Domain;

namespace Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;

public class VerifyOtpCommand : IRequest<BaseResponse<VerifyOtpCommandResponse>>
{
    public Guid UserId { get; set; }
    public string Otp { get; set; }
    public OtpType OtpType { get; set; }
}