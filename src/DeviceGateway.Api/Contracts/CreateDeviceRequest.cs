namespace DeviceGateway.Api.Contracts;

public record CreateDeviceRequest(string Name, Guid BrandId);