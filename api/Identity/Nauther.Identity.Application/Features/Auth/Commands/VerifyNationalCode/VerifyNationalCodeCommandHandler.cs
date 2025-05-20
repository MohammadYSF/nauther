using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;

public class VerifyNationalCodeCommandHandler(
    IUserService userService,
    IRequestValidator requestValidator,
    IMapper mapper) :
    IRequestHandler<VerifyNationalCodeCommand, BaseResponse<VerifyNationalCodeCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<VerifyNationalCodeCommandResponse>> Handle(VerifyNationalCodeCommand request,
        CancellationToken cancellationToken)
    {
        var validationResponse =
            await _requestValidator.ValidateAsync<VerifyNationalCodeCommand,VerifyNationalCodeCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<VerifyNationalCodeCommandResponse>
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };

        var user = await _userService.GetUserByNationalCode(request.NationalCode, cancellationToken);
        return new BaseResponse<VerifyNationalCodeCommandResponse>()
        {
            StatusCode = user.StatusCode,
            Message = user.Message,
            Data = user.Data
        };
    }
}