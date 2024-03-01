using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Services;

public sealed class CarService : ICarService
{
    private readonly AppDbContext _dbContext;

    public CarService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        Car car=new()
        {
            Name=command.Name,
            Model=command.Model,
            EnginePower=command.EnginePower
        };
        //await _dbContext.Set<Car>().AddAsync(car,cancellationToken);
        await _dbContext.AddAsync(car,cancellationToken);
        await _dbContext.SaveChangesAsync();
    }
}
