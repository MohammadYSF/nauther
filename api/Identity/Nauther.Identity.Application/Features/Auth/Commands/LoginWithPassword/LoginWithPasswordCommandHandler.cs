using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;

namespace Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;

public class LoginWithPasswordCommandHandler(IUserService userService, IRequestValidator requestValidator,
    IMapper mapper) :
    IRequestHandler<LoginWithPasswordCommand, BaseResponse<LoginWithPasswordCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<LoginWithPasswordCommandResponse>> Handle(LoginWithPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var loginUser = await _userService.LoginWithPassword(request, cancellationToken);
        return new BaseResponse<LoginWithPasswordCommandResponse>()
        {
            StatusCode = loginUser.StatusCode,
            Message = loginUser.Message,
            Data = loginUser.Data
        };
    }
}