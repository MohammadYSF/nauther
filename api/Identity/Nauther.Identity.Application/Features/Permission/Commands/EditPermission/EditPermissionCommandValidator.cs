using FluentValidation;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Permission.Commands.EditPermission;

public class EditPermissionCommandValidator : AbstractValidator<EditPermissionCommand>
{
    public EditPermissionCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().NotEmpty().WithMessage(Messages.PermissionNameRequired)
            .MaximumLength(25).WithMessage(Messages.PermissionMaxLenght);

        RuleFor(r => r.DisplayName)
        .NotNull().NotEmpty().WithMessage(Messages.PermissionDisplayNameRequired)
        .MaximumLength(25).WithMessage(Messages.PermissionDisplayNameMaxLenght);
    }
}