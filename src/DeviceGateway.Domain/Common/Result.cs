namespace DeviceGateway.Domain.Common;

/// <summary>
/// Represents the result of an operation, which can be either a success or a failure. In case of failure, it contains an error object with details about the failure.
/// </summary>
public class Result
{
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    private Result() { }
    private Result(Error error) => Error = error;

    public static Result Success() => new();
    public static Result Failure(string error) => new(new Error(error));
}

/// <summary>
/// Represents the result of an operation that returns a value, which can be either a success or a failure. In case of success, it contains the value; in case of failure, it contains an error object with details about the failure.
/// </summary>
/// <typeparam name="TValue">The type of the value returned in case of success.</typeparam>
public class Result<TValue>
{
    public TValue? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(TValue value) => Value = value;
    private Result(Error error) => Error = error;

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(string error) => new(new Error(error));
}

public record Error(string Description);