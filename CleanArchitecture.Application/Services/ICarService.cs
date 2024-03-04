using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public interface ICarService
{
    Task CreateAsync(CreateCarCommand command, CancellationToken cancellationToken);
    Task<IList<Car>> GetAllAsync();
    Task<GetCarListResponse> GetListAsync(int page = 1, int pageSize=10);

}
