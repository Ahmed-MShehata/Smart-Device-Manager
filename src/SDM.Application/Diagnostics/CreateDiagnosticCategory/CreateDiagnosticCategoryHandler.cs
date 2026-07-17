using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Diagnostics.CreateDiagnosticCategory;

/// <summary>
/// Handles <see cref="CreateDiagnosticCategoryCommand"/>.
/// </summary>
public sealed class CreateDiagnosticCategoryHandler : ICommandHandler<CreateDiagnosticCategoryCommand, CreateDiagnosticCategoryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="CreateDiagnosticCategoryHandler"/>.</summary>
    public CreateDiagnosticCategoryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<CreateDiagnosticCategoryResponse>> Handle(
        CreateDiagnosticCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = new DiagnosticCategory(command.Name, command.Description, command.IconName);

        await _unitOfWork.DiagnosticCategories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateDiagnosticCategoryResponse>.Success(
            new CreateDiagnosticCategoryResponse { Id = category.Id, Name = category.Name },
            "Diagnostic category created successfully.");
    }
}
