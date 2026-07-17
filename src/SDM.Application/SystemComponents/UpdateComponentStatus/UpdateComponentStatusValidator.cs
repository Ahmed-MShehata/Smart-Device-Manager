using FluentValidation;

namespace SDM.Application.SystemComponents.UpdateComponentStatus;

/// <summary>
/// Validates <see cref="UpdateComponentStatusCommand"/>.
/// </summary>
public sealed class UpdateComponentStatusValidator : AbstractValidator<UpdateComponentStatusCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateComponentStatusValidator"/>.</summary>
    public UpdateComponentStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Component ID is required.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("A valid component status must be selected.");
    }
}
