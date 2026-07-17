using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Settings.DeleteSetting;

/// <summary>
/// Handles <see cref="DeleteSettingCommand"/>.
/// Performs a hard delete on the configuration setting.
/// </summary>
public sealed class DeleteSettingHandler : ICommandHandler<DeleteSettingCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="DeleteSettingHandler"/>.</summary>
    public DeleteSettingHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        DeleteSettingCommand command,
        CancellationToken cancellationToken)
    {
        var setting = await _unitOfWork.Settings.GetByIdAsync(command.Id, cancellationToken);
        if (setting is null)
            return Result.Failure(Error.NotFound("Setting"));

        _unitOfWork.Settings.Remove(setting);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Setting deleted successfully.");
    }
}
