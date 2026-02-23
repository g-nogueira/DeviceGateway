namespace DeviceGateway.Domain.Common;

public class Result<TValue>
{
    public TValue? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(TValue value) => Value = value;
    private Result(Error error) => Error = error;

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);
}

public record Error(string Code, string Description);