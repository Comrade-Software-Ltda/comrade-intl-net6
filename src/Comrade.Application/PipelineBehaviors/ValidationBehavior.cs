using Comrade.Application.Bases;
using FluentValidation;
using MediatR;

namespace Comrade.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : SingleResultDto<EntityDto>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;


    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        //pre
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (failures.Any())
        {
            var validationResult = new SingleResultDto<EntityDto>(failures);
            return Task.FromResult(validationResult as TResponse)!;
        }

        //next
        return next();

        //pos
    }
}