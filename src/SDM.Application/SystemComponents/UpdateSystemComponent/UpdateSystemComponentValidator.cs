using FluentValidation;

namespace SDM.Application.SystemComponents.UpdateSystemComponent;

/// <summary>
/// Validates <see cref="UpdateSystemComponentCommand"/>.
/// </summary>
public sealed class UpdateSystemComponentValidator : AbstractValidator<UpdateSystemComponentCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateSystemComponentValidator"/>.</summary>
    public UpdateSystemComponentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Component ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Component name is required.")
            .MaximumLength(200).WithMessage("Component name must not exceed 200 characters.");

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("Version is required.")
            .MaximumLength(50).WithMessage("Version must not exceed 50 characters.");

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("File path is required.")
            .MaximumLength(1000).WithMessage("File path must not exceed 1000 characters.");

        RuleFor(x => x.SilentInstallCommand)
            .NotEmpty().WithMessage("Silent install command is required.")
            .MaximumLength(500).WithMessage("Silent install command must not exceed 500 characters.");

        RuleFor(x => x.SHA256)
            .NotEmpty().WithMessage("SHA-256 hash is required.")
            .Length(64).WithMessage("SHA-256 hash must be exactly 64 characters.")
            .Matches("^[a-fA-F0-9]{64}$").WithMessage("SHA-256 hash must be a valid hex string.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Component size must be greater than zero.");
    }
}
