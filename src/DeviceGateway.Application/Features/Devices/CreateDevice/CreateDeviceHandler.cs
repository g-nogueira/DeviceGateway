using DeviceGateway.Domain.Common;
using DeviceGateway.Domain.Entities;
using DeviceGateway.Domain.Interfaces;
using MediatR;

namespace DeviceGateway.Application.Features.Devices.CreateDevice;

public class CreateDeviceHandler(IDeviceRepository repository) : IRequestHandler<CreateDeviceCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var incomingData = new Device(request.Name, request.BrandId);

        var idResult = await repository.AddAsync(incomingData, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return idResult;
    }
}