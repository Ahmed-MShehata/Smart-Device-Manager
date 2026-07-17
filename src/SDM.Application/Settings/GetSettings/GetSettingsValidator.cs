using FluentValidation;

namespace SDM.Application.Settings.GetSettings;

/// <summary>
/// Validates <see cref="GetSettingsQuery"/>.
/// </summary>
public sealed class GetSettingsValidator : AbstractValidator<GetSettingsQuery>
{
    /// <summary>Initializes validation rules.</summary>
    public GetSettingsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}
