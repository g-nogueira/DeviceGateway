using DeviceGateway.Api.Contracts;
using DeviceGateway.Application.Features.Devices.Common;
using DeviceGateway.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace DeviceGateway.Api.Mappings;

[Mapper]
[UseStaticMapper(typeof(DeviceStateConverter))]
public partial class DeviceMapper
{
    public partial GetDeviceResponse MapToGetDeviceResponse(Device device);

    // If you have a list of devices (like in your GetAll method)
    public partial IEnumerable<DeviceResponse> MapToResponses(IEnumerable<Device> devices);
}