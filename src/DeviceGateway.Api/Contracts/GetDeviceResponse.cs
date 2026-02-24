namespace DeviceGateway.Api.Contracts;

public record GetDeviceResponse(Guid Id, string Name, Guid BrandId);