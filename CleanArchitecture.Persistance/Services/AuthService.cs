using CleanArchitecture.Application.Features.Auth.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task RegisterAsync(RegisterCommand command)
    {
        AppUser user=new AppUser
        {
            UserName = command.UserName,
            Email = command.Email
        };
        IdentityResult createResult= await _userManager.CreateAsync(user,command.Password);
        if (!createResult.Succeeded)
            throw new Exception(createResult.Errors.First().Description);
    }
}
