using FluentValidation;
using Nauther.Framework.Application.Helpers;
using Nauther.Identity.Application.Features.Auth.Commands.VerifyNationalCode;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Auth.Commands.VerifyOtp;

public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(x => x.Otp)
            .NotNull().WithMessage(Messages.OtpRequired)
            .NotEmpty().WithMessage(Messages.OtpRequired)
            .Length(6).WithMessage(Messages.OtpLength);
    }
}