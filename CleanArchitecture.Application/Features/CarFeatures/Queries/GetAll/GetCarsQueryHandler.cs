using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetAll;

public sealed class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, IList<Car>>
{
    private readonly ICarService _carService;

    public GetCarsQueryHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IList<Car>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        IList<Car> cars= await _carService.GetAllAsync();
        return cars;
    }
}
