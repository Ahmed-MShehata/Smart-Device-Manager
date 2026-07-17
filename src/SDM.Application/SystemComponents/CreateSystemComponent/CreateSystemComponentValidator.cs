using FluentValidation;

namespace SDM.Application.SystemComponents.CreateSystemComponent;

/// <summary>
/// Validates <see cref="CreateSystemComponentCommand"/>.
/// </summary>
public sealed class CreateSystemComponentValidator : AbstractValidator<CreateSystemComponentCommand>
{
    /// <summary>Initializes a new instance of <see cref="CreateSystemComponentValidator"/>.</summary>
    public CreateSystemComponentValidator()
    {
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
