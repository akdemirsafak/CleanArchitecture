using CleanArchitecture.Application.Features.Users.Commands.AssignRoleToUser;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task AssignRole(AssignRoleToUserCommand command)
    {
        var user= await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");
        var role= await _roleManager.FindByIdAsync(command.RoleId);
        if (role == null)
            throw new Exception("Rol bulunamadı.");
        
        IdentityResult identityResult=await _userManager.AddToRoleAsync(user, role.Name);
        if (!identityResult.Succeeded)
            throw new Exception("Kullanıcıya rol atanamadı.");
    }
}
