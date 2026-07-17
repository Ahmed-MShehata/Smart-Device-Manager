using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.DeleteDiagnosticChoice;

/// <summary>
/// Handles <see cref="DeleteDiagnosticChoiceCommand"/>.
/// </summary>
public sealed class DeleteDiagnosticChoiceHandler : ICommandHandler<DeleteDiagnosticChoiceCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteDiagnosticChoiceHandler"/>.</summary>
    public DeleteDiagnosticChoiceHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteDiagnosticChoiceCommand command, CancellationToken cancellationToken)
    {
        var choice = await _unitOfWork.DiagnosticChoices.GetByIdAsync(command.Id, cancellationToken);
        if (choice is null)
            return Result.Failure(Error.NotFound("DiagnosticChoice"));

        _unitOfWork.DiagnosticChoices.Remove(choice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic choice deleted successfully.");
    }
}
