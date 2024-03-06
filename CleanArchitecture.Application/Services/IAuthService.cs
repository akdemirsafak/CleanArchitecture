using CleanArchitecture.Application.Features.Auth.Commands.Login;
using CleanArchitecture.Application.Features.Auth.Commands.Register;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public interface IAuthService
{
    Task<AppUser> RegisterAsync(RegisterCommand command);
    Task<TokenResponse> LoginAsync(LoginCommand command, CancellationToken cancellationToken);

}
