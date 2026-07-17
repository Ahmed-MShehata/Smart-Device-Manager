using FluentValidation;

namespace SDM.Application.Settings.CreateSetting;

/// <summary>
/// Validates <see cref="CreateSettingCommand"/>.
/// </summary>
public sealed class CreateSettingValidator : AbstractValidator<CreateSettingCommand>
{
    /// <summary>Initializes a new instance of <see cref="CreateSettingValidator"/>.</summary>
    public CreateSettingValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Setting key is required.")
            .MaximumLength(100).WithMessage("Key must not exceed 100 characters.")
            .Matches("^[a-zA-Z0-9_.]+$").WithMessage("Key can only contain letters, digits, dots, and underscores.");

        RuleFor(x => x.Value)
            .MaximumLength(4000).WithMessage("Value must not exceed 4000 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
