using CleanArchitecture.Application.Abstraction;
using CleanArchitecture.Domain.Dtos;
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
    private readonly UserManager<AppUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(
        IOptions<JwtOptions> jwtOptions,
        UserManager<AppUser> userManager)
    {
        _jwtOptions = jwtOptions.Value;
        _userManager = userManager;
    }

    public async Task<TokenResponse> CreateTokenAsync(AppUser user)
    {
        SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

        var claims=new List<Claim>{
            new(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(_jwtOptions.Audiences.Select(audience=> new Claim(JwtRegisteredClaimNames.Aud,audience)));

        DateTime accessTokenExpiration = DateTime.Now.AddHours(1);

        JwtSecurityToken jwtSecurityToken= new (
            _jwtOptions.Issuer,
            //_jwtOptions.Audiences[0], 1 taneden fazla olduğu için claim'lere ekledim.
            claims:claims,
            notBefore:DateTime.Now,
            expires:accessTokenExpiration,
            signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature)
            );


        string refreshToken= Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        DateTime refreshTokenExpiration = accessTokenExpiration.AddMinutes(15);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = refreshTokenExpiration;
        await _userManager.UpdateAsync(user);
        string token= new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var tokenResponse=new TokenResponse(token, accessTokenExpiration, refreshToken, refreshTokenExpiration);
        return tokenResponse;
    }
}
