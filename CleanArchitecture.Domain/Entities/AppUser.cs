using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities;

public sealed class AppUser:IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}
