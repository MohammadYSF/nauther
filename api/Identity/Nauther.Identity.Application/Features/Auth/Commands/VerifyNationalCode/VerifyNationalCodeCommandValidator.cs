using FluentValidation;
using Nauther.Framework.Application.Helpers;
using Nauther.Framework.Shared.Constants;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;

public class VerifyNationalCodeCommandValidator : AbstractValidator<VerifyNationalCodeCommand>
{
    public VerifyNationalCodeCommandValidator()
    {
        RuleFor(x => x.NationalCode)
            .NotNull().WithMessage(Messages.NationalCodeRequired)
            .NotEmpty().WithMessage(Messages.NationalCodeRequired)
            .MaximumLength(10).WithMessage(Messages.NationalCodeMaxLength)
            .Must(NationalCodeValidation.IsValidNationalCode).WithMessage(Messages.InvalidNationalCode);
    }
}