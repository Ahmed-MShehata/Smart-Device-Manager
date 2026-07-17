using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.UpdateDiagnosticQuestion;

/// <summary>
/// Handles <see cref="UpdateDiagnosticQuestionCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticQuestionHandler : ICommandHandler<UpdateDiagnosticQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticQuestionHandler"/>.</summary>
    public UpdateDiagnosticQuestionHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateDiagnosticQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _unitOfWork.DiagnosticQuestions.GetByIdAsync(command.Id, cancellationToken);
        if (question is null)
            return Result.Failure(Error.NotFound("DiagnosticQuestion"));

        question.Update(command.QuestionText, command.OrderIndex);

        _unitOfWork.DiagnosticQuestions.Update(question);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic question updated successfully.");
    }
}
