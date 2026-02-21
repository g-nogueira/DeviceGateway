using DeviceGateway.Api;
using DeviceGateway.Application;
using DeviceGateway.Domain;
using DeviceGateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddApi(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddDomain(builder.Configuration)
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();