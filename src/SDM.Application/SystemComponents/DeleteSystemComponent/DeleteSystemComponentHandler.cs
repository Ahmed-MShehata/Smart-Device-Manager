using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.SystemComponents.DeleteSystemComponent;

/// <summary>
/// Handles <see cref="DeleteSystemComponentCommand"/>.
/// Performs a soft delete by marking the <see cref="Domain.Entities.SystemComponent"/> as <c>Deprecated</c>.
/// </summary>
public sealed class DeleteSystemComponentHandler : ICommandHandler<DeleteSystemComponentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteSystemComponentHandler"/>.</summary>
    public DeleteSystemComponentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        DeleteSystemComponentCommand command,
        CancellationToken cancellationToken)
    {
        var component = await _unitOfWork.SystemComponents.GetByIdAsync(command.Id, cancellationToken);
        if (component is null)
            return Result.Failure(Error.NotFound("SystemComponent"));

        // Soft delete
        component.SetStatus(ComponentStatus.Inactive);

        _unitOfWork.SystemComponents.Update(component);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("System component deleted (marked inactive) successfully.");
    }
}
