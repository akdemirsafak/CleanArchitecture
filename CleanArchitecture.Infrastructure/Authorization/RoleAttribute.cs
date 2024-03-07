using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Authorization;

public sealed class RoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;
    private readonly UserManager<AppUser> _userManager;
    public RoleAttribute(string role,
        UserManager<AppUser> userManager)
    {
        _role = role;
        _userManager = userManager;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Buradaki context'te header dahil okuma yapabiliriz. 
        var userId= context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user=  _userManager.FindByIdAsync(userId).Result;
        var userRoles= _userManager.GetRolesAsync(user).Result;

        var contains=userRoles.Contains(_role);
        if (!contains)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
