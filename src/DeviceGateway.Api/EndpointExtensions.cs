using DeviceGateway.Api.Features.Devices;

namespace DeviceGateway.Api;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapDevicesEndpoints();
    } 
}