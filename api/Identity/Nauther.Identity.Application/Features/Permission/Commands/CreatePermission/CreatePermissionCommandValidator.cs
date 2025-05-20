using FluentValidation;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Permission.Commands.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().NotEmpty().WithMessage(Messages.PermissionNameRequired)
            .MaximumLength(25).WithMessage(Messages.PermissionMaxLenght);
    }
}