using CleanArchitecture.Application.Abstraction;
using CleanArchitecture.Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;
using CleanArchitecture.Application.Features.Auth.Commands.Login;
using CleanArchitecture.Application.Features.Auth.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(UserManager<AppUser> userManager,
        IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<TokenResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand command)
    {
        AppUser user= await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            throw new Exception("User not found.");

        if (user.RefreshToken != command.RefreshToken)
            throw new Exception("Refresh token geçerli değil.");

        if (user.RefreshTokenExpiration < DateTime.Now)
            throw new Exception("Refresh token'ın süresi dolmuş.");

        TokenResponse token= await _jwtProvider.CreateTokenAsync(user);
        return token;
    }

    public async Task<TokenResponse> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        AppUser? user= await _userManager.FindByEmailAsync(command.UserNameOrPassword);
        if (user is null)
            user = await _userManager.FindByNameAsync(command.UserNameOrPassword);

        if (user is null)
            throw new Exception("Kullanıcı bulunamadı.");

        bool isTrue=await _userManager.CheckPasswordAsync(user,command.Password);
        if (!isTrue)
            throw new Exception("Kullanıcı adı ve şifrenizi kontrol ediniz..");

        TokenResponse tokenResponse= await _jwtProvider.CreateTokenAsync(user);
        return tokenResponse;
    }

    public async Task<AppUser> RegisterAsync(RegisterCommand command)
    {
        AppUser user=new AppUser
        {
            UserName = command.UserName,
            Email = command.Email
        };
        IdentityResult createResult= await _userManager.CreateAsync(user,command.Password);
        if (!createResult.Succeeded)
            throw new Exception(createResult.Errors.First().Description);
        return user;
    }
}
