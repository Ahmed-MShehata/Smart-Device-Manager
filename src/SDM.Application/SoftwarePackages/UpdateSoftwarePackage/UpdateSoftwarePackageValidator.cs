using FluentValidation;

namespace SDM.Application.SoftwarePackages.UpdateSoftwarePackage;

/// <summary>FluentValidation rules for <see cref="UpdateSoftwarePackageCommand"/>.</summary>
public sealed class UpdateSoftwarePackageValidator : AbstractValidator<UpdateSoftwarePackageCommand>
{
    private static readonly string[] AllowedCategories = ["Application", "Driver"];

    public UpdateSoftwarePackageValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Package ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Software name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Must(c => AllowedCategories.Contains(c))
            .WithMessage("Category must be 'Application' or 'Driver'.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

        // When a new file is being uploaded, version must also be provided
        When(x => !string.IsNullOrWhiteSpace(x.NewSetupFileUrl), () =>
        {
            RuleFor(x => x.NewVersion)
                .NotEmpty().WithMessage("New version is required when replacing the setup file.")
                .MaximumLength(50).WithMessage("Version must not exceed 50 characters.");
        });
    }
}
