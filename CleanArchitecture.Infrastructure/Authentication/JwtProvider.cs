using CleanArchitecture.Application.Abstraction;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Options.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string CreateTokenAsync(AppUser user)
    {
        SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

        var claims=new Claim[]{
            new Claim(ClaimTypes.Email,user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken jwtSecurityToken= new (
            _jwtOptions.Issuer,
            //_jwtOptions.Audiences[0],
            claims:claims,
            notBefore:DateTime.Now,
            expires:DateTime.Now.AddHours(1),
            signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature)
            );
        string token= new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }
}
