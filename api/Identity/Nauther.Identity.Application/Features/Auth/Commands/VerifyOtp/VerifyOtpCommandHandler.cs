using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;

public class VerifyOtpCommandHandler(
    IUserService userService,
    IRequestValidator requestValidator,
    IMapper mapper) :
    IRequestHandler<VerifyOtpCommand, BaseResponse<VerifyOtpCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<VerifyOtpCommandResponse>> Handle(VerifyOtpCommand request,
        CancellationToken cancellationToken)
    {
        var validationResponse =
            await _requestValidator.ValidateAsync<VerifyOtpCommand,VerifyOtpCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<VerifyOtpCommandResponse>
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };

        var user = await _userService.VerifyOtp(request, cancellationToken);
        return new BaseResponse<VerifyOtpCommandResponse>()
        {
            StatusCode = user.StatusCode,
            Message = user.Message,
            Data = user.Data
        };
    }
}