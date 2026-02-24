using DeviceGateway.Api.Contracts;
using DeviceGateway.Api.Extensions;
using DeviceGateway.Application.Features.Devices.CreateDevice;
using DeviceGateway.Application.Features.Devices.GetDevice;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeviceGateway.Api.Mappings;

namespace DeviceGateway.Api.Features.Devices;

public static class DevicesEndpoints
{
    public static WebApplication MapDevicesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/devices")
            .WithTags("Devices")
            .WithOpenApi()
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", CreateDevice)
            .WithName("CreateDevice")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:guid}", GetDevice)
            .WithName("GetDevice")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }

    /// <summary>
    /// Creates a new device.
    /// </summary>
    private static async Task<IResult> CreateDevice(
        [FromBody] CreateDeviceRequest request,
        IMediator mediator,
        CancellationToken ct)
    {
        var command = new CreateDeviceCommand(request.Name, request.BrandId);
        var result = await mediator.Send(command, ct);

        return result.ToHttpResult(
            routeName: "GetDevice",
            routeValues: r => new { id = r });
    }

    /// <summary>
    /// Fetches a single device by its unique identifier.
    /// </summary>
    private static async Task<IResult> GetDevice(
        Guid id,
        IMediator mediator,
        CancellationToken ct)
    {
        var query = new GetDeviceQuery(id);
        var result = await mediator.Send(query, ct);
        
        if (result.IsSuccess)
        {
            var mapper = new DeviceMapper();
            var device = result.Value!;
            var response = mapper.MapToGetDeviceResponse(device);
            return Results.Ok(response);
        }

        return result.ToHttpResult();
    }
}