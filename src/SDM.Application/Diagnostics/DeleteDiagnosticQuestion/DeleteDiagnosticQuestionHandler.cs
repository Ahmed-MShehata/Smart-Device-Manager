using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.DeleteDiagnosticQuestion;

/// <summary>
/// Handles <see cref="DeleteDiagnosticQuestionCommand"/>.
/// Relying on EF Core cascade deletes to automatically sweep choices hooked to this question ID.
/// </summary>
public sealed class DeleteDiagnosticQuestionHandler : ICommandHandler<DeleteDiagnosticQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteDiagnosticQuestionHandler"/>.</summary>
    public DeleteDiagnosticQuestionHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteDiagnosticQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _unitOfWork.DiagnosticQuestions.GetByIdAsync(command.Id, cancellationToken);
        if (question is null)
            return Result.Failure(Error.NotFound("DiagnosticQuestion"));

        _unitOfWork.DiagnosticQuestions.Remove(question);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic question deleted successfully.");
    }
}
