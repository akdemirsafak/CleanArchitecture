using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArchitecture.Application.Behaivors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(!_validators.Any())
            return await next();

        var context= new ValidationContext<TRequest>(request);
        var errorsDictionary= _validators.Select(x=>x.Validate(context))
            .SelectMany(x=>x.Errors)
            .Where(s=> s!=null)
            .GroupBy(x=>x.PropertyName,x=>x.ErrorMessage,
            (propertyName,errorMessage)=>new
            {
                Key=propertyName,
                Value=errorMessage.Distinct().ToArray()
            }).ToDictionary(x=>x.Key,x=>x.Value[0]);
        if (errorsDictionary.Any())
        {
            var errors = errorsDictionary.Select(x=>new ValidationFailure
            {
                PropertyName=x.Value,
                ErrorCode=x.Key
            });
            throw new ValidationException(errors);
        }
       return await next();
    }
}
