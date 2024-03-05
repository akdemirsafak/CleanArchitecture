using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandValidator:AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        //RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("Şifre adı en az 1 adet büyük harf içermelidir.");
        //RuleFor(x => x.Password).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir.");
        //RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir.");
        //RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir.");
        //RuleFor(x => x.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir.");
    }
}
