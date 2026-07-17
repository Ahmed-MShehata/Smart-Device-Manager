using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SystemComponents.UpdateComponentStatus;

/// <summary>
/// Handles <see cref="UpdateComponentStatusCommand"/>.
/// Modifies only the availability status of a system component.
/// </summary>
public sealed class UpdateComponentStatusHandler : ICommandHandler<UpdateComponentStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateComponentStatusHandler"/>.</summary>
    public UpdateComponentStatusHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdateComponentStatusCommand command,
        CancellationToken cancellationToken)
    {
        var component = await _unitOfWork.SystemComponents.GetByIdAsync(command.Id, cancellationToken);
        if (component is null)
            return Result.Failure(Error.NotFound("SystemComponent"));

        component.SetStatus(command.Status);

        _unitOfWork.SystemComponents.Update(component);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success($"Component status changed to {command.Status}.");
    }
}
