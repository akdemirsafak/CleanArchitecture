using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetAll;

public sealed class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<Car>>
{
    private readonly ICarService _carService;

    public GetCarsQueryHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<List<Car>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        var cars= await _carService.GetAllAsync();
        return cars;
    }
}
