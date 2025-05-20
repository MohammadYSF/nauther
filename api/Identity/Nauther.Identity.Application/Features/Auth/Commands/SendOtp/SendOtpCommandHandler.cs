using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Auth.Commands.SendOtp;

public class SendOtpCommandHandler(
    IUserService userService,
    IRequestValidator requestValidator,
    IMapper mapper) :
    IRequestHandler<SendOtpCommand, BaseResponse<SendOtpCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<SendOtpCommandResponse>> Handle(SendOtpCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.SendOtp(request, cancellationToken);
        return new BaseResponse<SendOtpCommandResponse>()
        {
            StatusCode = user.StatusCode,
            Message = user.Message
        };
    }
}