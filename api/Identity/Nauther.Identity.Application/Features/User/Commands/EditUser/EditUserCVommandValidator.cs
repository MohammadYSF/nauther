using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.User.Commands.EditUser
{
    public class EditUserCVommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCVommandValidator()
        {
            When(command => !string.IsNullOrEmpty(command.Password), () =>
            {
                RuleFor(x => x.Password)
                    .NotNull().WithMessage(Messages.PasswordRequired)
                    .NotEmpty().WithMessage(Messages.PasswordRequired)
                    .MinimumLength(8).WithMessage(Messages.PasswordMinLength)
                    .Matches("[A-Z]").WithMessage(Messages.PasswordUpperCase)
                    .Matches("[a-z]").WithMessage(Messages.PasswordLowerCase)
                    .Matches(@"\d").WithMessage(Messages.PasswordDigit)
                    .Matches("[^a-zA-Z0-9]").WithMessage(Messages.PasswordSpecialChar);

                RuleFor(x => x.ConfirmPassword)
                    .NotNull().WithMessage(Messages.ConfirmPasswordRequired)
                    .NotEmpty().WithMessage(Messages.ConfirmPasswordRequired)
                    .Equal(x => x.Password)
                    .WithMessage(Messages.ConfirmPasswordMismatch);
            });
        }
    }

}
