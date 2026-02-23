using DeviceGateway.Domain.Common;
using MediatR;

namespace DeviceGateway.Application.Features.Devices.CreateDevice;

public class CreateDeviceHandler : IRequestHandler<CreateDeviceCommand, Result<CreateDeviceResponse>>
{
    public Task<Result<CreateDeviceResponse>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}