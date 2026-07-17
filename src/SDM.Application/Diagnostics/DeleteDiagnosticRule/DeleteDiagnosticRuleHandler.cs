using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.DeleteDiagnosticRule;

/// <summary>
/// Handles <see cref="DeleteDiagnosticRuleCommand"/>.
/// </summary>
public sealed class DeleteDiagnosticRuleHandler : ICommandHandler<DeleteDiagnosticRuleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteDiagnosticRuleHandler"/>.</summary>
    public DeleteDiagnosticRuleHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteDiagnosticRuleCommand command, CancellationToken cancellationToken)
    {
        var rule = await _unitOfWork.DiagnosticRules.GetByIdAsync(command.Id, cancellationToken);
        if (rule is null)
            return Result.Failure(Error.NotFound("DiagnosticRule"));

        _unitOfWork.DiagnosticRules.Remove(rule);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic rule deleted successfully.");
    }
}
