using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Diagnostics.AddDiagnosticRule;

/// <summary>
/// Handles <see cref="AddDiagnosticRuleCommand"/>.
/// </summary>
public sealed class AddDiagnosticRuleHandler : ICommandHandler<AddDiagnosticRuleCommand, AddDiagnosticRuleResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="AddDiagnosticRuleHandler"/>.</summary>
    public AddDiagnosticRuleHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<AddDiagnosticRuleResponse>> Handle(AddDiagnosticRuleCommand command, CancellationToken cancellationToken)
    {
        var categoryExists = await _unitOfWork.DiagnosticCategories.ExistsAsync(command.CategoryId, cancellationToken);
        if (!categoryExists)
            return Result<AddDiagnosticRuleResponse>.Failure(Error.NotFound("DiagnosticCategory"));

        var rule = new DiagnosticRule(
            command.CategoryId,
            command.MinScore,
            command.MaxScore,
            command.Result,
            command.Solution);

        await _unitOfWork.DiagnosticRules.AddAsync(rule, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AddDiagnosticRuleResponse>.Success(
            new AddDiagnosticRuleResponse { RuleId = rule.Id },
            "Diagnostic rule added successfully.");
    }
}
