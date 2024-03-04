using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Persistance.Mapping;

public sealed class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCarCommand, Car>();
        CreateMap<GetCarListResponse,Paginate<Car>>().ReverseMap();
    }
}
