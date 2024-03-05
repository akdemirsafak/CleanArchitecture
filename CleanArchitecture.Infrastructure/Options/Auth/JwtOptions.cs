namespace CleanArchitecture.Infrastructure.Options.Auth;

public sealed class JwtOptions
{
    public string Issuer { get; set; }
    public List<string> Audiences { get; set; }
    public string SecurityKey { get; set; }
}
