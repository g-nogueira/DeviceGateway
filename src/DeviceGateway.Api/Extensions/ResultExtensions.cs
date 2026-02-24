using DeviceGateway.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGateway.Api.Extensions;

/// <summary>
/// Extension methods to convert domain <see cref="Result"/> and <see cref="Result{TValue}"/> to HTTP <see cref="IResult"/>.
/// Maps <see cref="ErrorType"/> to appropriate HTTP status codes following RFC 7807 Problem Details.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{TValue}"/> to an HTTP result, returning the value on success
    /// or a ProblemDetails response on failure.
    /// </summary>
    public static IResult ToHttpResult<TValue>(this Result<TValue> result, string? routeName = null, Func<TValue, object>? routeValues = null)
    {
        if (!result.IsSuccess) return ToProblemResult(result.Error!);

        if (routeName is not null && routeValues is not null)
        {
            return Results.CreatedAtRoute(routeName, routeValues(result.Value!), result.Value);
        }

        return Results.Ok(result.Value);

    }

    /// <summary>
    /// Converts a <see cref="Result"/> (no value) to an HTTP result, returning 204 NoContent on success
    /// or a ProblemDetails response on failure.
    /// </summary>
    public static IResult ToHttpResult(this Result result)
    {
        if (result.IsSuccess)
            return Results.NoContent();

        return ToProblemResult(result.Error!);
    }

    private static IResult ToProblemResult(Error error)
    {
        var (statusCode, title) = error.Type switch
        {
            ErrorType.NotFound => (StatusCodes.Status404NotFound, "Not Found"),
            ErrorType.Conflict => (StatusCodes.Status409Conflict, "Conflict"),
            ErrorType.Validation => (StatusCodes.Status400BadRequest, "Bad Request"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        return Results.Problem(new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = error.Description
        });
    }
}
