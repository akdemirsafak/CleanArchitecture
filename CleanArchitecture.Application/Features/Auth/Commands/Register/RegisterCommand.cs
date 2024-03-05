using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string UserName,
    string Password):IRequest<MessageResponse>;
