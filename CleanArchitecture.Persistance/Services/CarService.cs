using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
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

    public async Task<IList<Car>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<GetCarListResponse> GetListAsync(int page=1, int pageSize=10, string searchText=null)
    {
        var queryable=_repository.GetList();
        if (!String.IsNullOrEmpty(searchText))
            queryable=queryable.Where(x => x.Name.ToLower() == searchText.ToLower());

        var paginated= Paginate<Car>.ToPagedList(queryable, page, pageSize);
        return _mapper.Map<GetCarListResponse>(paginated);
    }
}
