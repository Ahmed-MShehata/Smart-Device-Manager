using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.AdminUsers.DeleteAdminUser;

/// <summary>
/// Handles <see cref="DeleteAdminUserCommand"/>.
/// Performs a soft delete by deactivating the admin account.
/// Physical row is never removed.
/// </summary>
public sealed class DeleteAdminUserHandler : ICommandHandler<DeleteAdminUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAdminUserHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteAdminUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdminUsers.GetByIdAsync(command.Id, cancellationToken);

        if (user is null)
            return Result.Failure(Error.NotFound("AdminUser"));

        if (!user.IsActive)
            return Result.Failure(Error.Conflict("AdminUser", "This admin account is already deactivated."));

        user.Deactivate();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Admin account deactivated (soft-deleted) successfully.");
    }
}
