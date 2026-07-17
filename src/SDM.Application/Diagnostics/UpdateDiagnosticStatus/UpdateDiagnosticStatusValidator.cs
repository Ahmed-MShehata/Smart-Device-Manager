using FluentValidation;

namespace SDM.Application.Diagnostics.UpdateDiagnosticStatus;

/// <summary>
/// Validates <see cref="UpdateDiagnosticStatusCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticStatusValidator : AbstractValidator<UpdateDiagnosticStatusCommand>
{
    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticStatusValidator"/>.</summary>
    public UpdateDiagnosticStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required.");
    }
}
