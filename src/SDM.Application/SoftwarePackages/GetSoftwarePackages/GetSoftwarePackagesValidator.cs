using FluentValidation;

namespace SDM.Application.SoftwarePackages.GetSoftwarePackages;

/// <summary>
/// Validates <see cref="GetSoftwarePackagesQuery"/>.
/// </summary>
public sealed class GetSoftwarePackagesValidator : AbstractValidator<GetSoftwarePackagesQuery>
{
    /// <summary>Initializes validation rules.</summary>
    public GetSoftwarePackagesValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}
