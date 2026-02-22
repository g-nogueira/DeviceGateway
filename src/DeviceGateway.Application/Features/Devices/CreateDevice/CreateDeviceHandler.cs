using MediatR;

namespace DeviceGateway.Application.Features.Devices.CreateDevice;

public class CreateDeviceHandler : IRequestHandler<CreateDeviceCommand, CreateDeviceResponse>
{
    public Task<CreateDeviceResponse> Handle(CreateDeviceCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}