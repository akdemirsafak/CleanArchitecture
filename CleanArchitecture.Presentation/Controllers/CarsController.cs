using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAll;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public class CarsController : ApiController
{
    public CarsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCarCommand request,CancellationToken cancellationToken)
    {
        var messageResponse=await _mediator.Send(request, cancellationToken);
        return Ok(messageResponse);
    }
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
