using FluentValidation;

namespace DeviceGateway.Api.Middleware;

/// <summary>
/// An Endpoint Filter that uses FluentValidation to validate incoming requests before they reach the MediatR Handlers.
/// </summary>
/// <param name="validator"></param>
/// <typeparam name="T"></typeparam>
public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // Find the Command in the arguments
        var argument = context.Arguments.OfType<T>().FirstOrDefault();
        if (argument is null) return await next(context);

        // Validate
        var result = await validator.ValidateAsync(argument);
        if (!result.IsValid)
        {
            // Short-circuit if invalid
            return Results.ValidationProblem(result.ToDictionary());
        }

        // Continue to the MediatR Handler
        return await next(context);
    }
}