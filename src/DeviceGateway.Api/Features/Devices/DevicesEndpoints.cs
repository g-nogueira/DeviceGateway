namespace DeviceGateway.Api.Features.Devices;

public static class DevicesEndpoints
{
    public static WebApplication MapDevicesEndpoints(this WebApplication app)
    {
        // var group = app.MapGroup("/devices")
        //     .WithTags("Devices")
        //     .WithOpenApi();
        //
        // group.MapPost("/devices", CreateDevice)
        //     .WithName("CreateDevice")
        //     .WithTags("Devices");

        return app;
    }
}