using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.UpdateDiagnosticRule;

/// <summary>
/// Handles <see cref="UpdateDiagnosticRuleCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticRuleHandler : ICommandHandler<UpdateDiagnosticRuleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticRuleHandler"/>.</summary>
    public UpdateDiagnosticRuleHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateDiagnosticRuleCommand command, CancellationToken cancellationToken)
    {
        var rule = await _unitOfWork.DiagnosticRules.GetByIdAsync(command.Id, cancellationToken);
        if (rule is null)
            return Result.Failure(Error.NotFound("DiagnosticRule"));

        rule.Update(command.MinScore, command.MaxScore, command.Result, command.Solution);

        _unitOfWork.DiagnosticRules.Update(rule);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic rule updated successfully.");
    }
}
