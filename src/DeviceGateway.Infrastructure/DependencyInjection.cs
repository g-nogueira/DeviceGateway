using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DeviceGateway.Infrastructure;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            
            
            
            // Register infrastructure services here
            return services;
        }
}