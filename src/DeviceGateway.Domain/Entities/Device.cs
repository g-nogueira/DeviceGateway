using DeviceGateway.Domain.Common;

namespace DeviceGateway.Domain.Entities;

public class Device
{
    ///  Id using a time marked UUID / GuidV7
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    /// BrandId using a time marked UUID / GuidV7
    /// <remarks>A Foreign Key (FK) should be far more performant than a string on a Relational Database</remarks>
    public Guid BrandId { get; private set; }

    public DeviceState State { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    /// Checks whether the state of the device can be updated based on its current state.
    /// <remarks>
    /// This method was implemented when I made a confusion between which properties could be updated,
    /// but I'll leave it here as it makes sense. In a real case, it would need to be discussed if it should be implemented or not.
    /// </remarks>
    public (bool, string?) CanUpdateState() =>
        State switch
        {
            DeviceState.Available => (true, null),
            DeviceState.InUse => (true, null),
            DeviceState.Inactive => (false, "Cannot change state of an inactive device."),
            _ => (false, "Invalid device state.")
        };

    public (bool, string?) CanUpdateName()
    {
        if (State == DeviceState.InUse)
            return (false, "Cannot change name of a device that is currently in use.");

        return (true, null);
    }
    
    public (bool, string?) CanUpdateBrand()
    {
        if (State == DeviceState.InUse)
            return (false, "Cannot change brand of a device that is currently in use.");

        return (true, null);
    }

    /// Updates the name of the device. If the device is currently in use, its name cannot be changed.
    public Result UpdateName(string newName)
    {
        var (canUpdate, errorMessage) = CanUpdateName();

        if (!canUpdate)
            return Result.Failure(errorMessage!, ErrorType.Validation);

        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure("Device name cannot be empty.", ErrorType.Validation);

        Name = newName;
        return Result.Success();
    }

    /// Updates both the name and brandId of the device. The same rules apply as when updating them individually.
    public Result UpdateDetails(string newName, Guid newBrandId)
    {
        // Snapshot the current state before making any changes
        var originalBrandId = BrandId;
        var originalName = Name;

        // Perform updates
        BrandId = newBrandId;
        Name = newName;

        // Validate the new state against business invariants
        var (isBrandValid, stateError) = CanUpdateBrand();
        var (isNameValid, nameError) = CanUpdateName();

        if (isBrandValid && isNameValid && !string.IsNullOrWhiteSpace(newName)) return Result.Success();

        // Rollback if any of the rules is violated
        BrandId = originalBrandId;
        Name = originalName;

        return Result.Failure(stateError ?? nameError ?? "Invalid details", ErrorType.Validation);
    }
    
    public Result Delete()
    {
        if (State == DeviceState.InUse)
            return Result.Failure("Cannot delete a device that is currently in use.", ErrorType.Validation);

        State = DeviceState.Inactive;
        DeletedAt = DateTimeOffset.UtcNow;

        return Result.Success();
    }
}

/// <summary>
/// Represents the possible states of a <see cref="Device"/>.
/// </summary>
public enum DeviceState
{
    Available = 1,
    InUse = 2,
    Inactive = 3
}