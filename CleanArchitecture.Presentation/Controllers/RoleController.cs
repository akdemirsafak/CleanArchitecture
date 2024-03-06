using CleanArchitecture.Application.Features.Roles.Commands.CreateRole;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public class RoleController : ApiController
{
    public RoleController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}
