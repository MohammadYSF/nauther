using FluentValidation;
using Nauther.Identity.Application.Features.Role.Commands.CreateRole;
using Nauther.Identity.Application.Resources;

namespace Nauther.Identity.Application.Features.Group.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().WithMessage(Messages.GroupNameRequired)
            .NotEmpty().WithMessage(Messages.GroupNameRequired)
            .MaximumLength(35).WithMessage(Messages.GroupMaxLenght);
    }
}