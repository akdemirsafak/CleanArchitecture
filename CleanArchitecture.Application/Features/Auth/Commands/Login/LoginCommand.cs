using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(
    string UserNameOrPassword,
    string Password) : IRequest<TokenResponse>;
