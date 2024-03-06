using CleanArchitecture.Application.Features.Users.Commands.AssignRoleToUser;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CleanArchitecture.Presentation.Controllers;

public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
