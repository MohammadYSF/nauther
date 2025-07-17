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
public class Dima_RegisterUserCommand_Dto
{
    public string Id { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public List<Guid> Roles { get; set; } = [];
    public List<Guid> Permissions { get; set; } = [];
}
public class Dima_RegisterUserCommand : IRequest<BaseResponse>
{
    public Dima_RegisterUserCommand(Dima_RegisterUserCommand_Dto dto, bool check)
    {
        this.Check = check;
        this.Id = dto.Id;
        this.Password = dto.Password;
        this.ConfirmPassword = dto.ConfirmPassword;
        this.Roles = dto.Roles;
        this.Permissions = dto.Permissions;
    }
    public bool Check { get; set; }
    public string Id { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public List<Guid> Roles { get; set; } = [];
    public List<Guid> Permissions { get; set; } = [];
}