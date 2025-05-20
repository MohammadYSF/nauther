using FluentValidation;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Role.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().NotEmpty().WithMessage(Messages.RoleNameRequired)
            .MaximumLength(25).WithMessage(Messages.RoleMaxLenght);
    }
}