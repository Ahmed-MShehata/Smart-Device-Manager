using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Settings.UpdateSetting;

/// <summary>
/// Handles <see cref="UpdateSettingCommand"/>.
/// Updates an existing setting's value and metadata.
/// </summary>
public sealed class UpdateSettingHandler : ICommandHandler<UpdateSettingCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateSettingHandler"/>.</summary>
    public UpdateSettingHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdateSettingCommand command,
        CancellationToken cancellationToken)
    {
        var setting = await _unitOfWork.Settings.GetByIdAsync(command.Id, cancellationToken);
        if (setting is null)
            return Result.Failure(Error.NotFound("Setting"));

        setting.UpdateValue(command.Value);
        setting.UpdateMetadata(command.Description, command.IsPublic);

        _unitOfWork.Settings.Update(setting);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Setting updated successfully.");
    }
}
