using FluentValidation;

namespace CleanArchitecture.Application.Features.Roles.Commands.CreateRole;

public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x=> x.Name)
            .NotNull().WithMessage("Rol adı girmelisiniz.")
            .NotEmpty().WithMessage("Rol adı boş olamaz.");
    }
}
