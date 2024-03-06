using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Commands.AssignRoleToUser;

public sealed class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, MessageResponse>
{
    private readonly IUserService _userService;

    public AssignRoleToUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<MessageResponse> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.AssignRole(request);
        return new("Rol kullanıcıya başarıyla atandı.");
    }
}
