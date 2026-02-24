using DeviceGateway.Domain.Common;
using DeviceGateway.Domain.Entities;
using DeviceGateway.Domain.Interfaces;
using DeviceGateway.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeviceGateway.Infrastructure.Repositories;

public class DeviceRepository(DeviceDbContext context) : IDeviceRepository
{
    /// <inheritdoc/> 
    public async Task<Result<Device>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var trackedDevice = await context.Devices.FirstOrDefaultAsync(d => d.Id == id, ct);

        if (trackedDevice == null)
            return Result<Device>.Failure($"Device with ID {id} not found.");

        return Result<Device>.Success(trackedDevice);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Device>> GetAllAsync(int skip, int take, CancellationToken ct = default) =>
        context.Devices
            .AsNoTracking() // No tracking for read-only queries for improved performance
            .OrderBy(d => d.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct)
            .ContinueWith(IEnumerable<Device> (t) => t.Result, ct);

    /// <inheritdoc/>
    public async Task AddAsync(Device device, CancellationToken ct = default) =>
        await context.Devices.AddAsync(device, ct);

    /// <inheritdoc/>
    public async Task<Result> UpdateAsync(Device incomingDevice, CancellationToken ct)
    {
        var trackedDevice = await context.Devices
            .FirstOrDefaultAsync(d => d.Id == incomingDevice.Id, ct);

        if (trackedDevice == null)
            return Result.Failure($"Device with ID {incomingDevice.Id} not found.");

        var result = trackedDevice.UpdateDetails(newName: incomingDevice.Name, newBrandId: incomingDevice.BrandId);

        return result;
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var trackedDevice = await context.Devices.FirstOrDefaultAsync(d => d.Id == id, ct);

        if (trackedDevice == null)
            return Result.Failure("Device not found.");

        var result = trackedDevice.Delete();

        return result;
    }

    /// <inheritdoc/>
    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default) =>
        context.Devices.AnyAsync(d => d.Id == id, ct);

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken ct) => await context.SaveChangesAsync(ct);
}