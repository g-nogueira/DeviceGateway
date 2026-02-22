using DeviceGateway.Api;
using DeviceGateway.Application;
using DeviceGateway.Domain;
using DeviceGateway.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

// Two-stage Logger Initialization. See more on https://github.com/serilog/serilog-aspnetcore#two-stage-initialization
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);

    // Setup Logging
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console(new JsonFormatter());
    });

    Log.Information("Configuring services");
    // Add Services
    builder.Services
        .AddOpenApi()
        .AddApi(builder.Configuration)
        .AddApplication(builder.Configuration)
        .AddDomain(builder.Configuration)
        .AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    Log.Information("Configuring middleware");
    // Add Endpoints
    app.MapEndpoints();

    // Configure Dev only env
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    Log.Information("Starting application");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}