using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Diagnostics.UpdateDiagnosticCategory;

/// <summary>
/// Handles <see cref="UpdateDiagnosticCategoryCommand"/>.
/// </summary>
public sealed class UpdateDiagnosticCategoryHandler : ICommandHandler<UpdateDiagnosticCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateDiagnosticCategoryHandler"/>.</summary>
    public UpdateDiagnosticCategoryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateDiagnosticCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.DiagnosticCategories.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Result.Failure(Error.NotFound("DiagnosticCategory"));

        category.Update(command.Name, command.Description, command.IconName);

        _unitOfWork.DiagnosticCategories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Diagnostic category updated successfully.");
    }
}
