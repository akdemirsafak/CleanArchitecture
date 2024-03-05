using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Abstraction;

public interface IJwtProvider
{
    string CreateToken(AppUser user); //Token ile birlikte rollerin taşınması yanlıştır.Kullanıcı bilgileriyle db'den sorgulayıp kullanmak daha güvenlidir.
}
