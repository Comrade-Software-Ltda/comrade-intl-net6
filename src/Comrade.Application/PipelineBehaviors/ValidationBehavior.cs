using Comrade.Application.Bases;
using FluentValidation;
using MediatR;

namespace Comrade.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : SingleResultDto<EntityDto>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        //pre
        var context = new ValidationContext<TRequest>(request);
        var failures = validators
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
