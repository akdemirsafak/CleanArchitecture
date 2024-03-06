using CleanArchitecture.Application.Features.Roles.Commands.CreateRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task CreateAsync(CreateRoleCommand command)
    {
        AppRole role=new()
        {
            Name=command.Name
        };
        IdentityResult createResult=await _roleManager.CreateAsync(role);
        if (!createResult.Succeeded)
            throw new Exception("Rol eklenemedi.");
    }
}
