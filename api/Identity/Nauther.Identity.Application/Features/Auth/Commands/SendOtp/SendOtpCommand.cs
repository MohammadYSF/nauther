using MediatR;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;
using Nauther.Identity.Domain;

namespace Nauther.Identity.Application.Features.Auth.Commands.SendOtp;

public class SendOtpCommand : IRequest<BaseResponse<SendOtpCommandResponse>>
{
    public Guid UserId { get; set; }
}