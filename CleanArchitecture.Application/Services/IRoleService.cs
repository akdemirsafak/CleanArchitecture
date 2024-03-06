using CleanArchitecture.Application.Features.Roles.Commands.CreateRole;

namespace CleanArchitecture.Application.Services;

public interface IRoleService
{
    Task CreateAsync(CreateRoleCommand command);
}
