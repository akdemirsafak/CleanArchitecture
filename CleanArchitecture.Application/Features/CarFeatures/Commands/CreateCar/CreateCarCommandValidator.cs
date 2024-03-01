using FluentValidation;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;

public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 32).WithMessage("Araç adı 1-32 karakter aralığında olmalıdır.");
        RuleFor(x => x.EnginePower)
            .NotNull()
            .GreaterThan(1);
        RuleFor(x => x.Model)
            .NotNull();

    }
}
