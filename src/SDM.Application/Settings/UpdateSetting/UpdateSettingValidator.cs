using FluentValidation;

namespace SDM.Application.Settings.UpdateSetting;

/// <summary>
/// Validates <see cref="UpdateSettingCommand"/>.
/// </summary>
public sealed class UpdateSettingValidator : AbstractValidator<UpdateSettingCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateSettingValidator"/>.</summary>
    public UpdateSettingValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Setting ID is required.");

        RuleFor(x => x.Value)
            .MaximumLength(4000).WithMessage("Value must not exceed 4000 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
