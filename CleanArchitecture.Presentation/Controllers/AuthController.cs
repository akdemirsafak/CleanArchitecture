using CleanArchitecture.Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;
using CleanArchitecture.Application.Features.Auth.Commands.Login;
using CleanArchitecture.Application.Features.Auth.Commands.Register;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginCommand command,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command,cancellationToken));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateTokenByRefreshToken(CreateNewTokenByRefreshTokenCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}