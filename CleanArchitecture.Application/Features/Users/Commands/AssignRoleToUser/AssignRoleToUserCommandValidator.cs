using FluentValidation;

namespace CleanArchitecture.Application.Features.Users.Commands.AssignRoleToUser;

public sealed class AssignRoleToUserCommandValidator : AbstractValidator<AssignRoleToUserCommand>
{
    public AssignRoleToUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.RoleId)
            .NotNull()
            .NotEmpty();
    }
}
