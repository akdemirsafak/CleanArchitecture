using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;

public sealed record GetCarListQuery(
int PageNumber=1,
int PageSize= 10,
string searchText=null) : IRequest<GetCarListResponse>;

