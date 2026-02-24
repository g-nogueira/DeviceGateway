using DeviceGateway.Api.Contracts;
using DeviceGateway.Domain.Entities;

namespace DeviceGateway.Api.Mappings;

public static class DeviceStateConverter
{
    public static DeviceStateResponse MapToResponse(DeviceState state)
    {
        // Ideally these values should come from a config file like appsettings, Consul, or similar
        return state switch
        {
            DeviceState.Available => new DeviceStateResponse(1, "Available"),
            DeviceState.InUse => new DeviceStateResponse(2, "In Use"),
            DeviceState.Inactive => new DeviceStateResponse(3, "Inactive"),
            _ => new DeviceStateResponse(0, "Unknown")
        };
    }
}