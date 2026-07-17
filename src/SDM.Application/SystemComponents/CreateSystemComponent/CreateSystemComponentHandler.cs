using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.SystemComponents.CreateSystemComponent;

/// <summary>
/// Handles <see cref="CreateSystemComponentCommand"/>.
/// Integrates a new <see cref="SystemComponent"/> into the database via UnitOfWork.
/// </summary>
public sealed class CreateSystemComponentHandler : ICommandHandler<CreateSystemComponentCommand, CreateSystemComponentResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="CreateSystemComponentHandler"/>.</summary>
    public CreateSystemComponentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<CreateSystemComponentResponse>> Handle(
        CreateSystemComponentCommand command,
        CancellationToken cancellationToken)
    {
        var component = new SystemComponent(
            command.Name,
            command.Version,
            command.FilePath,
            command.SilentInstallCommand,
            command.SHA256.ToLowerInvariant(),
            command.Size,
            command.RequiresRestart);

        await _unitOfWork.SystemComponents.AddAsync(component, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateSystemComponentResponse>.Success(
            new CreateSystemComponentResponse
            {
                Id        = component.Id,
                Name      = component.Name,
                Version   = component.Version,
                Status    = component.Status,
                CreatedAt = component.CreatedAt
            },
            "System component registered successfully.");
    }
}
