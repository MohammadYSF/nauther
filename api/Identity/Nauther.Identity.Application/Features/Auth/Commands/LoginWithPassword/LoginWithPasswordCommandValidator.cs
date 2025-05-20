using FluentValidation;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Auth.Commands.LoginWithPassword;

public class LoginWithPasswordCommandValidator : AbstractValidator<LoginWithPasswordCommand>
{
    public LoginWithPasswordCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(Messages.PasswordRequired);
    }
}