namespace DeviceGateway.Domain.Exceptions;

/// <summary>
/// The base exception for all domain exceptions. Domain exceptions occurs when business rules are violated.
/// </summary>
/// <param name="message"></param>
public class DomainException(string message) : Exception(message);