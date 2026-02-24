using FluentValidation;

namespace DeviceGateway.Application.Features.Devices.CreateDevice;

public class GetDeviceValidator : AbstractValidator<CreateDeviceCommand>
{
    public GetDeviceValidator()
    {
        // Rule for the Name property
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        // Rule for the BrandId
        RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("Brand ID is required.");
    }
}