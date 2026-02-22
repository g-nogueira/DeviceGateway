using DeviceGateway.Domain.Interfaces;
using DeviceGateway.Infrastructure.Data;
using DeviceGateway.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DeviceGateway.Infrastructure;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // We use DbContextPool instead of DbContext for better performance in high-load scenarios. It allows reusing DbContext instances from a pool, reducing the overhead of creating and disposing DbContext instances frequently.
            // When a DbContext is returned to the pool, the state of the context is reset and the reset chain disposes the DbConnection 
            services.AddDbContextPool<DeviceDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            
            return services;
        }
}