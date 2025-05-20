using AutoMapper;
using MediatR;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Services.Interfaces;


namespace Nauther.Identity.Application.Features.Auth.Commands.Register;

public class RegisterUserCommandHandler(
    IUserService userService,
    IRequestValidator requestValidator,
    IMapper mapper) :
    IRequestHandler<RegisterUserCommand, BaseResponse<RegisterUserCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<RegisterUserCommandResponse>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var validationResponse =
            await _requestValidator.ValidateAsync<RegisterUserCommand, RegisterUserCommandValidator>(request);
        if (validationResponse != null)
            return new BaseResponse<RegisterUserCommandResponse>
            {
                StatusCode = validationResponse.StatusCode,
                Message = validationResponse.Message,
                ValidationErrors = validationResponse.ValidationErrors
            };

        var newUser = await _userService.Register(request, cancellationToken);
        return new BaseResponse<RegisterUserCommandResponse>()
        {
            StatusCode = newUser.StatusCode,
            Message = newUser.Message,
            Data = newUser.Data
        };
    }
}