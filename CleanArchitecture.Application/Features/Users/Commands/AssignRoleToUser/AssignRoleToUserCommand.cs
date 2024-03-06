using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Commands.AssignRoleToUser;

public sealed record AssignRoleToUserCommand(string UserId, string RoleId):IRequest<MessageResponse>;
