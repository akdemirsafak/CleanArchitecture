using FluentValidation;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetList;

public sealed class GetListQueryValidator : AbstractValidator<GetCarListQuery>
{
    public GetListQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotNull()
            .GreaterThan(0);        
        RuleFor(x => x.PageSize)
            .NotNull()
            .GreaterThan(0);        
    }
}
