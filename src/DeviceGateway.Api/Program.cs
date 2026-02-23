using DeviceGateway.Api;
using DeviceGateway.Application;
using DeviceGateway.Domain;
using DeviceGateway.Infrastructure;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

// Two-stage Logger Initialization. See more on https://github.com/serilog/serilog-aspnetcore#two-stage-initialization
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(new JsonFormatter())
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
            .Enrich.FromLogContext();
    });

    // Setup Tracing & Metrics
    builder.Services.AddOpenTelemetry()
        .UseOtlpExporter()
        .WithTracing(tracingBuilder =>
        {
            tracingBuilder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("DeviceGateway.Api")
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DeviceGateway.Api"));
        })
        .WithMetrics(metricsBuilder =>
        {
            metricsBuilder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter("System.Runtime")
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DeviceGateway.Api"));
        });

    builder.Services.AddProblemDetails();

    Log.Information("Configuring services");
    // Add Services
    builder.Services
        .AddApi(builder.Configuration)
        .AddApplication(builder.Configuration)
        .AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    Log.Information("Configuring middleware");

    app.UseExceptionHandler();

    // Add Endpoints
    app.UseHttpsRedirection();
    app.MapEndpoints();

    // Configure Dev only env
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app
            .UseSwagger()
            .UseSwaggerUI();
    }


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