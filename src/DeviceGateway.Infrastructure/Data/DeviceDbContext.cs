using DeviceGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceGateway.Infrastructure.Data;

public class DeviceDbContext(DbContextOptions<DeviceDbContext> options) : DbContext(options) 
{
    public DbSet<Device> Devices => Set<Device>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeviceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}