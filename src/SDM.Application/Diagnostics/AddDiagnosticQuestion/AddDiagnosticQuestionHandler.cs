using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Diagnostics.AddDiagnosticQuestion;

/// <summary>
/// Handles <see cref="AddDiagnosticQuestionCommand"/>.
/// Creates the question and all its choices in a single transaction.
/// </summary>
public sealed class AddDiagnosticQuestionHandler : ICommandHandler<AddDiagnosticQuestionCommand, AddDiagnosticQuestionResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="AddDiagnosticQuestionHandler"/>.</summary>
    public AddDiagnosticQuestionHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<AddDiagnosticQuestionResponse>> Handle(
        AddDiagnosticQuestionCommand command,
        CancellationToken cancellationToken)
    {
        // Verify that the parent category exists
        var categoryExists = await _unitOfWork.DiagnosticCategories.ExistsAsync(command.CategoryId, cancellationToken);
        if (!categoryExists)
            return Result<AddDiagnosticQuestionResponse>.Failure(Error.NotFound("DiagnosticCategory"));

        var question = new DiagnosticQuestion(command.CategoryId, command.QuestionText, command.OrderIndex);

        await _unitOfWork.DiagnosticQuestions.AddAsync(question, cancellationToken);
        // Flush to get the generated question ID before adding choices
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Add all choices in bulk
        foreach (var choiceRequest in command.Choices)
        {
            var choice = new DiagnosticChoice(question.Id, choiceRequest.ChoiceText, choiceRequest.ScoreValue);
            await _unitOfWork.DiagnosticChoices.AddAsync(choice, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AddDiagnosticQuestionResponse>.Success(
            new AddDiagnosticQuestionResponse { QuestionId = question.Id, ChoiceCount = command.Choices.Count },
            "Diagnostic question added successfully.");
    }
}
