using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Services;

public sealed class CarService : ICarService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CarService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        Car car=_mapper.Map<Car>(command);
        //await _dbContext.Set<Car>().AddAsync(car,cancellationToken);
        await _dbContext.AddAsync(car,cancellationToken);
        await _dbContext.SaveChangesAsync();
    }
}
