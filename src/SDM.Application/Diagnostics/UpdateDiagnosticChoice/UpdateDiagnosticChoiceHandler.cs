using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.UpdateDiagnosticChoice;

/// <summary>
/// Handles <see cref="UpdateDiagnosticChoiceCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticChoiceHandler : ICommandHandler<UpdateDiagnosticChoiceCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticChoiceHandler"/>.</summary>
    public UpdateDiagnosticChoiceHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateDiagnosticChoiceCommand command, CancellationToken cancellationToken)
    {
        var choice = await _unitOfWork.DiagnosticChoices.GetByIdAsync(command.Id, cancellationToken);
        if (choice is null)
            return Result.Failure(Error.NotFound("DiagnosticChoice"));

        choice.Update(command.ChoiceText, command.ScoreValue);

        _unitOfWork.DiagnosticChoices.Update(choice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic choice updated successfully.");
    }
}
