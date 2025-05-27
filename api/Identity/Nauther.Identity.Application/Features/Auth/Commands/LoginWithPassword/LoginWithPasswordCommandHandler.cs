using System.Security.Claims;
using auther.Identity.Application.Services.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Framework.Application.Services.RequestValidatorService;
using Nauther.Framework.Infrastructure.Common.Constants;
using Nauther.Framework.Infrastructure.Services.JwtToken;
using Nauther.Framework.Shared.Responses;
using Nauther.Identity.Application.Features.User.Commands.CheckPassword;
using Nauther.Identity.Application.Resources;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;

public class LoginWithPasswordCommandHandler(IJwtTokenService jwtTokenService, IMediator mediator, IUserService userService, IRequestValidator requestValidator,
    IMapper mapper) :
    IRequestHandler<LoginWithPasswordCommand, BaseResponse<LoginWithPasswordCommandResponse>>
{
    private readonly IUserService _userService = userService;
    private readonly IRequestValidator _requestValidator = requestValidator;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    public async Task<BaseResponse<LoginWithPasswordCommandResponse>> Handle(LoginWithPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var checkPasswordCommand = new CheckPasswordCommand() { Password = request.Password, Username = request.Username };
        var checkPasswordResponse = await _mediator.Send(checkPasswordCommand, cancellationToken);
        if (checkPasswordResponse.StatusCode != 200)
        {
            return new BaseResponse<LoginWithPasswordCommandResponse>
            {
                StatusCode = checkPasswordResponse.StatusCode,
                Message = checkPasswordResponse.Message,
                Metadata = checkPasswordResponse.Metadata,
                ValidationErrors = checkPasswordResponse.ValidationErrors
            };
        }
        var claims = new List<Claim>
            {
            new Claim(CustomClaimTypes.Username, request.Username.ToString()),
            new Claim(CustomClaimTypes.UserId, checkPasswordResponse.Data.Id.ToString()),
            new Claim(CustomClaimTypes.Permissions, "testPermission")
        };
        string token = _jwtTokenService.GenerateToken(DateTime.UtcNow.AddDays(1), claims);
        return new BaseResponse<LoginWithPasswordCommandResponse>()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "",
            Data = new LoginWithPasswordCommandResponse
            {
                Access_Token = token
            }
        };

    }
}