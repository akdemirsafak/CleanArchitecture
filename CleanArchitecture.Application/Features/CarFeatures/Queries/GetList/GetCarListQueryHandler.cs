using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;

public sealed class GetCarListQueryHandler : IRequestHandler<GetCarListQuery, GetCarListResponse>
{
    private readonly ICarService _carService;

    public GetCarListQueryHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<GetCarListResponse> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
       return await _carService.GetListAsync(request.PageNumber,request.PageSize);
    }
}

