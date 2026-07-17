using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Settings.CreateSetting;

/// <summary>
/// Handles <see cref="CreateSettingCommand"/>.
/// Validates key uniqueness and saves the <see cref="Setting"/> into the database.
/// </summary>
public sealed class CreateSettingHandler : ICommandHandler<CreateSettingCommand, CreateSettingResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDb;

    /// <summary>Initializes a new instance of <see cref="CreateSettingHandler"/>.</summary>
    public CreateSettingHandler(IUnitOfWork unitOfWork, IReadDbContext readDb)
    {
        _unitOfWork = unitOfWork;
        _readDb = readDb;
    }

    /// <inheritdoc/>
    public async Task<Result<CreateSettingResponse>> Handle(
        CreateSettingCommand command,
        CancellationToken cancellationToken)
    {
        // Enforce unique keys
        var keyExists = await _readDb.Settings
            .AnyAsync(s => s.Key == command.Key, cancellationToken);
            
        if (keyExists)
            return Result<CreateSettingResponse>.Failure(new Error("Setting.DuplicateKey", $"A setting with the key '{command.Key}' already exists."));

        var setting = new Setting(
            command.Key,
            command.Value,
            command.Description,
            command.IsPublic);

        await _unitOfWork.Settings.AddAsync(setting, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateSettingResponse>.Success(
            new CreateSettingResponse { Id = setting.Id, Key = setting.Key },
            "Setting created successfully.");
    }
}
