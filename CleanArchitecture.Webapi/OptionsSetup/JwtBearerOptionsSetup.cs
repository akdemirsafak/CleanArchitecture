using CleanArchitecture.Infrastructure.Options.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.Webapi.OptionsSetup;

public sealed class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions> //JwtBearer lib.'den gelir.
{
    private readonly JwtOptions _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters=new TokenValidationParameters
        {
            ValidateIssuer=true, //Issuer kontrol edilsin mi
            ValidateAudience=true,
            ValidateLifetime=true,
            ValidIssuer=_jwtOptions.Issuer,
            ValidAudiences=_jwtOptions.Audiences,
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey))
        };
    }
}
