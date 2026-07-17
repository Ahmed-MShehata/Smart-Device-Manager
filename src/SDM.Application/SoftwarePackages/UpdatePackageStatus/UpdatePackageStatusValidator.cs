using FluentValidation;

namespace SDM.Application.SoftwarePackages.UpdatePackageStatus;

/// <summary>
/// FluentValidation validator for <see cref="UpdatePackageStatusCommand"/>.
/// </summary>
public sealed class UpdatePackageStatusValidator : AbstractValidator<UpdatePackageStatusCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdatePackageStatusValidator"/>.</summary>
    public UpdatePackageStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Package ID is required.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("A valid package status must be selected.");
    }
}
