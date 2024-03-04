using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;


public class GetCarListResponse : PaginatedResponse
{
    public List<Car> Items { get; set; }
}
