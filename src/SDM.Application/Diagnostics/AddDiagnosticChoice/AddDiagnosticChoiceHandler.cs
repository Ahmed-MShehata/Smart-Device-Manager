using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Diagnostics.AddDiagnosticChoice;

/// <summary>
/// Handles <see cref="AddDiagnosticChoiceCommand"/>.
/// </summary>
public sealed class AddDiagnosticChoiceHandler : ICommandHandler<AddDiagnosticChoiceCommand, AddDiagnosticChoiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="AddDiagnosticChoiceHandler"/>.</summary>
    public AddDiagnosticChoiceHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<AddDiagnosticChoiceResponse>> Handle(AddDiagnosticChoiceCommand command, CancellationToken cancellationToken)
    {
        var questionExists = await _unitOfWork.DiagnosticQuestions.ExistsAsync(command.QuestionId, cancellationToken);
        if (!questionExists)
            return Result<AddDiagnosticChoiceResponse>.Failure(Error.NotFound("DiagnosticQuestion"));

        var choice = new DiagnosticChoice(
            command.QuestionId,
            command.ChoiceText,
            command.ScoreValue);

        await _unitOfWork.DiagnosticChoices.AddAsync(choice, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AddDiagnosticChoiceResponse>.Success(
            new AddDiagnosticChoiceResponse { ChoiceId = choice.Id },
            "Diagnostic choice added successfully.");
    }
}
