using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGateway.Api;

/// <summary>
/// A gloobal exception handler that catches all exceptions and returns a standardized error response. This is a catch-all handler that can be used to log exceptions and return appropriate status codes based on the type of exception.
/// </summary>
/// <param name="logger"></param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    /// <summary>
    /// Handles exceptions and returns RFC 7807 Problem Details responses.
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        
        var (statusCode, title) = exception switch
        {
            
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };
        
        httpContext.Response.StatusCode = statusCode;
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}