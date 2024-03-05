using CleanArchitecture.Application.Features.Auth.Commands.Register;

namespace CleanArchitecture.Application.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterCommand command);

}
