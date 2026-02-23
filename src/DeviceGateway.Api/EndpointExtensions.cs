using DeviceGateway.Api.Features.Devices;

namespace DeviceGateway.Api;

public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapDevicesEndpoints();

        return app;
    } 
}