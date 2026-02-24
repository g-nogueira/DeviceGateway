using DeviceGateway.Domain.Common;
using MediatR;

namespace DeviceGateway.Application.Features.Devices.CreateDevice;

public record CreateDeviceCommand(string Name, Guid BrandId) : IRequest<Result<Guid>>;
