using DeviceGateway.Domain.Common;
using DeviceGateway.Domain.Entities;
using MediatR;

namespace DeviceGateway.Application.Features.Devices.GetDevice;

public record GetDeviceQuery(Guid Id) : IRequest<Result<Device>>;