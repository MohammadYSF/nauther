using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;

public class LoginWithPasswordCommand : IRequest<BaseResponse<LoginWithPasswordCommandResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}