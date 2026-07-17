using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SystemComponents.UpdateSystemComponent;

/// <summary>
/// Handles <see cref="UpdateSystemComponentCommand"/>.
/// Updates an existing system component's editable metadata.
/// </summary>
public sealed class UpdateSystemComponentHandler : ICommandHandler<UpdateSystemComponentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateSystemComponentHandler"/>.</summary>
    public UpdateSystemComponentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdateSystemComponentCommand command,
        CancellationToken cancellationToken)
    {
        var component = await _unitOfWork.SystemComponents.GetByIdAsync(command.Id, cancellationToken);
        if (component is null)
            return Result.Failure(Error.NotFound("SystemComponent"));

        component.Update(
            command.Name,
            command.Version,
            command.FilePath,
            command.SilentInstallCommand,
            command.SHA256.ToLowerInvariant(),
            command.Size,
            command.RequiresRestart);

        _unitOfWork.SystemComponents.Update(component);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("System component updated successfully.");
    }
}
