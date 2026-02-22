using DeviceGateway.Api.Middleware;
using DeviceGateway.Application.Features.Devices.CreateDevice;
using MediatR;

namespace DeviceGateway.Api.Features.Devices;

public static class DevicesEndpoints
{
    public static WebApplication MapDevicesEndpoints(this WebApplication app)
    {
        // var group = app.MapGroup("/devices")
        //     .WithTags("Devices")
        //     .WithOpenApi();
        //
        // group.MapPost("/devices",
        //         async (CreateDeviceCommand command, CancellationToken cancellationToken, IMediator mediator) =>
        //             await mediator.Send(command, cancellationToken))
        //     .AddEndpointFilter<ValidationFilter<CreateDeviceCommand>>()
        //     .WithName("CreateDevice")
        //     .WithTags("Devices");

        return app;
    }
}