using FluentValidation;
using Nauther.Framework.Application.Helpers;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.NationalCode)
                .NotNull().WithMessage(Messages.NationalCodeRequired)
                .NotEmpty().WithMessage(Messages.NationalCodeRequired)
                .MaximumLength(10).WithMessage(Messages.NationalCodeMaxLength)
                .Must(NationalCodeValidation.IsValidNationalCode).WithMessage(Messages.InvalidNationalCode);

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(Messages.PhoneNumberRequired)
                .NotEmpty().WithMessage(Messages.PhoneNumberRequired)
                .Matches(@"^(\+?[1-9]\d{1,14}|\d{10,15})$").WithMessage(Messages.PhoneNumberInvalid);

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
        }
    }
}