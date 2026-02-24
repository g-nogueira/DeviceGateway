namespace DeviceGateway.Domain.Common;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict
}

/// <summary>
/// Represents the result of an operation, which can be either a success or a failure. In case of failure, it contains an error object with details about the failure.
/// </summary>
public class Result
{
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    private Result()
    {
    }

    private Result(Error error) => Error = error;

    public static Result Success() => new();
    public static Result Failure(string error, ErrorType type = ErrorType.Validation) => new(new Error(error, type));
}

public class Result<TValue>
{
    public TValue? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;
    
    private Result(Error error) => Error = error;

    /// Represents the result of an operation that returns a value, which can be either a success or a failure. In case of success, it contains the value; in case of failure, it contains an error object with details about the failure.
    public static Result<TValue> Failure(string error, ErrorType type) => new(new Error(error, type));
}

public record Error(string Description, ErrorType Type);