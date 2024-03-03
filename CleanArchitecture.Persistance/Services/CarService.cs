using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.UnitOfWorks;

namespace CleanArchitecture.Persistance.Services;

public sealed class CarService : ICarService
{
    private readonly IGenericRepository<Car> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CarService(IGenericRepository<Car> repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        Car car=_mapper.Map<Car>(command);

        await _repository.CreateAsync(car);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}
