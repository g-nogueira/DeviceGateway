using DeviceGateway.Domain.Common;
using DeviceGateway.Domain.Entities;
using DeviceGateway.Domain.Interfaces;
using MediatR;

namespace DeviceGateway.Application.Features.Devices.GetDevice;

public class GetDeviceHandler(IDeviceRepository repository) : IRequestHandler<GetDeviceQuery, Result<Device>>
{
    public async Task<Result<Device>> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
    {
        var deviceResult = await repository.GetByIdAsync(request.Id, cancellationToken);
        return deviceResult;
    }
}