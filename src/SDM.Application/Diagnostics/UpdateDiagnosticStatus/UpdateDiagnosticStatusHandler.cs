using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.UpdateDiagnosticStatus;

/// <summary>
/// Handles <see cref="UpdateDiagnosticStatusCommand"/>.
/// Sets category visibility for the customer application.
/// </summary>
public sealed class UpdateDiagnosticStatusHandler : ICommandHandler<UpdateDiagnosticStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticStatusHandler"/>.</summary>
    public UpdateDiagnosticStatusHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateDiagnosticStatusCommand command, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DiagnosticCategories.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Result.Failure(Error.NotFound("DiagnosticCategory"));

        if (command.IsActive)
            category.Activate();
        else
            category.Deactivate();

        _unitOfWork.DiagnosticCategories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(command.IsActive ? "Diagnostic category activated." : "Diagnostic category deactivated.");
    }
}
