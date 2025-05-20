using MediatR;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.Auth.Commands.Register;

public class RegisterUserCommand : IRequest<BaseResponse<RegisterUserCommandResponse>>
{
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}