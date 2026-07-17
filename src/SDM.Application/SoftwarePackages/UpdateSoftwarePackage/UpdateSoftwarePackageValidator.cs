using FluentValidation;

namespace SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

/// <summary>
/// FluentValidation validator for <see cref="UpdateSoftwarePackageCommand"/>.
/// Invoked automatically by <c>ValidationBehavior</c>.
/// </summary>
public sealed class UpdateSoftwarePackageValidator : AbstractValidator<UpdateSoftwarePackageCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateSoftwarePackageValidator"/>.</summary>
    public UpdateSoftwarePackageValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Package ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Package name is required.")
            .MaximumLength(200).WithMessage("Package name must not exceed 200 characters.");

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("Version is required.")
            .MaximumLength(50).WithMessage("Version must not exceed 50 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage("File path is required.")
            .MaximumLength(1000).WithMessage("File path must not exceed 1000 characters.");

        RuleFor(x => x.SilentInstallCommand)
            .NotEmpty().WithMessage("Silent install command is required.")
            .MaximumLength(500).WithMessage("Silent install command must not exceed 500 characters.");

        RuleFor(x => x.DetectionRule)
            .NotEmpty().WithMessage("Detection rule is required.")
            .MaximumLength(1000).WithMessage("Detection rule must not exceed 1000 characters.");

        RuleFor(x => x.SHA256)
            .NotEmpty().WithMessage("SHA-256 hash is required.")
            .Length(64).WithMessage("SHA-256 hash must be exactly 64 characters.")
            .Matches("^[a-fA-F0-9]{64}$").WithMessage("SHA-256 hash must be a valid hex string.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Package size must be greater than zero.");

        RuleFor(x => x.InstallerType)
            .IsInEnum().WithMessage("A valid installer type must be selected.");
    }
}
