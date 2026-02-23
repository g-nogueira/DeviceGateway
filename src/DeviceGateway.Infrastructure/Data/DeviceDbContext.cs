using Microsoft.EntityFrameworkCore;

namespace DeviceGateway.Infrastructure.Data;

public class DeviceDbContext(DbContextOptions<DeviceDbContext> options) : DbContext(options);