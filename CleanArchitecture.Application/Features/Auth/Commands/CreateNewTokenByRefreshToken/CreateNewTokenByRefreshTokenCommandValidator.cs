using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenByRefreshTokenCommandValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
{
    public CreateNewTokenByRefreshTokenCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("{PropertyName} boş olamaz.")
            .NotEmpty().WithMessage("{PropertyName} boş olamaz.");

        RuleFor(x => x.RefreshToken)
            .NotNull().WithMessage("{PropertyName} boş olamaz.")
            .NotEmpty().WithMessage("{PropertyName} boş olamaz.");
    }
}
