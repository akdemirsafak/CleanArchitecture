using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAll;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;
using CleanArchitecture.Infrastructure.Authorization;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public class CarsController : ApiController
{
    public CarsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    [RoleFilter("Admin")]
    public async Task<IActionResult> Create(CreateCarCommand request,CancellationToken cancellationToken)
    {
        var messageResponse=await _mediator.Send(request, cancellationToken);
        return Ok(messageResponse);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var messageResponse=await _mediator.Send(new GetCarsQuery());
        return Ok(messageResponse);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> GetList(GetCarListQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
}
