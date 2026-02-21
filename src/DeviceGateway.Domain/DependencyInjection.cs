using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceGateway.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        return services;
    }
}