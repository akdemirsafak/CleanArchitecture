using CleanArchitecture.Application.Features.Users.Commands.AssignRoleToUser;

namespace CleanArchitecture.Application.Services;

public interface IUserService
{
    Task AssignRole(AssignRoleToUserCommand command);
}
