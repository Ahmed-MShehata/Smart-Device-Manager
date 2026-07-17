using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.SystemComponents.RemoveComponentFile;

/// <summary>Handles <see cref="RemoveComponentFileCommand"/>.</summary>
public sealed class RemoveComponentFileHandler : ICommandHandler<RemoveComponentFileCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="RemoveComponentFileHandler"/>.</summary>
    public RemoveComponentFileHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(RemoveComponentFileCommand command, CancellationToken cancellationToken)
    {
        var file = await _unitOfWork.ComponentFiles.GetByIdAsync(command.Id, cancellationToken);
        if (file is null)
            return Result.Failure(Error.NotFound("ComponentFile"));

        _unitOfWork.ComponentFiles.Remove(file);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("File reference removed successfully.");
    }
}
