using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetAll;

public sealed record GetCarsQuery():IRequest<List<Car>>;
