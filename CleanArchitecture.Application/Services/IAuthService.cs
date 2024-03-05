using CleanArchitecture.Application.Features.Auth.Commands.Register;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public interface IAuthService
{
    Task<AppUser> RegisterAsync(RegisterCommand command);

}
