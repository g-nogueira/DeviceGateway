using FluentValidation;
using MediatR;

namespace DeviceGateway.Application.Common.Behaviors;

/// <summary>
/// A <see cref="IPipelineBehavior{TRequest,TResponse}"/> that executes all the validators for a given <typeparamref name="TRequest"/> and [DOES SOMETHING] if any of the validations fail.
/// </summary>
/// <remarks>
/// See more about <see cref="IPipelineBehavior{TRequest,TResponse}"/> on https://github.com/LuckyPennySoftware/MediatR/wiki/Behaviors <br/>
/// See validators on namespace <see cref="DeviceGateway.Application.Features"/>
/// </remarks>
/// <param name="validators"></param>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Do nothing if no validations
        if (!validators.Any()) return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        // Execute all validators for the TRequest type
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Gather failures
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            // TODO: Implement Validation failure handling
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}