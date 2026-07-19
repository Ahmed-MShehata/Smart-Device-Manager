using FluentValidation;

namespace SDM.Application.SoftwarePackages.CreateSoftwarePackage;

/// <summary>FluentValidation validator for <see cref="CreateSoftwarePackageCommand"/>.</summary>
public sealed class CreateSoftwarePackageValidator : AbstractValidator<CreateSoftwarePackageCommand>
{
    private static readonly string[] AllowedCategories = ["Application", "Driver"];

    public CreateSoftwarePackageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Software name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Must(c => AllowedCategories.Contains(c))
            .WithMessage("Category must be 'Application' or 'Driver'.");

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("Version is required.")
            .MaximumLength(50).WithMessage("Version must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

        RuleFor(x => x.SetupFileUrl)
            .NotEmpty().WithMessage("Setup file is required.");
    }
}
