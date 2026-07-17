using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.AdminUsers.UpdateAdminStatus;

/// <summary>
/// Handles <see cref="UpdateAdminStatusCommand"/>.
/// Toggles account access via the <see cref="SDM.Domain.Entities.AdminUser"/> domain methods.
/// </summary>
public sealed class UpdateAdminStatusHandler : ICommandHandler<UpdateAdminStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateAdminStatusHandler"/>.</summary>
    public UpdateAdminStatusHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateAdminStatusCommand command, CancellationToken cancellationToken)
    {
        var adminUser = await _unitOfWork.AdminUsers.GetByIdAsync(command.Id, cancellationToken);
        if (adminUser is null)
            return Result.Failure(Error.NotFound("AdminUser"));

        if (command.IsActive)
            adminUser.Activate();
        else
            adminUser.Deactivate();

        _unitOfWork.AdminUsers.Update(adminUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(command.IsActive ? "Admin account activated." : "Admin account deactivated.");
    }
}
