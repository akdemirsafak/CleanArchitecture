using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandValidator:AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserNameOrPassword)
            .NotNull().WithMessage("Kullanıcı adı veya email boş olamaz.")
            .NotEmpty().WithMessage("Kullanıcı adı veya email boş olamaz.")
            .MinimumLength(3).WithMessage("{ProperyName} minimum 3 karakter olmalıdır.");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Lütfen şifrenizi giriniz.")
            .NotEmpty().WithMessage("Lütfen şifrenizi giriniz.");
        
        RuleFor(x => x.Password)
            .Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir.");
        
        RuleFor(x => x.Password)
            .Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir.");
        
        RuleFor(x => x.Password)
            .Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir.");
        
        RuleFor(x => x.Password)
            .Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir.");
        
        RuleFor(x => x.Password)
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir.");
    }
}
