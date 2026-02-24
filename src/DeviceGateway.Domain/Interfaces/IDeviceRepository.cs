using DeviceGateway.Domain.Common;
using DeviceGateway.Domain.Entities;

namespace DeviceGateway.Domain.Interfaces;

public interface IDeviceRepository
{
    /// <summary>
    /// Retrieves a device by its unique identifier. Returns null if the device is not found.
    /// </summary>
    Task<Result<Device>> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Retrieves a paginated list of devices.
    /// </summary>
    /// <param name="skip">Number of devices to skip for pagination.</param>
    /// <param name="take">Number of devices to take for pagination.</param>
    Task<IEnumerable<Device>> GetAllAsync(int skip, int take, CancellationToken ct = default);

    /// <summary>
    /// Adds a new device to the repository.
    /// </summary>
    Task<Result<Guid>> AddAsync(Device device, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing device in the repository. The device must already exist; otherwise, a failure result is returned.
    /// </summary>
    Task<Result> UpdateAsync(Device device, CancellationToken ct = default);

    /// <summary>
    /// Soft deletes a device by its unique identifier.
    /// </summary>
    Task<Result> DeleteAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Checks if a device with the specified unique identifier exists in the repository.
    /// </summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    
    /// <summary>
    /// Commits all changes made to the repository.
    /// </summary>
    Task SaveChangesAsync(CancellationToken ct);
}