using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.AdminUsers.UpdateAdminUser;

/// <summary>Handles <see cref="UpdateAdminUserCommand"/>.</summary>
public sealed class UpdateAdminUserHandler : ICommandHandler<UpdateAdminUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _db;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateAdminUserHandler(IUnitOfWork unitOfWork, IReadDbContext db, IPasswordHasher passwordHasher)
    {
        _unitOfWork     = unitOfWork;
        _db             = db;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(UpdateAdminUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AdminUsers
            .GetByIdAsync(command.Id, cancellationToken);

        if (user is null)
            return Result.Failure(Error.NotFound("AdminUser"));

        // Enforce username uniqueness (excluding self)
        var usernameTaken = await _db.AdminUsers
            .AnyAsync(u => u.Username == command.Username && u.Id != command.Id, cancellationToken);

        if (usernameTaken)
            return Result.Failure(Error.Conflict("AdminUser", $"Username '{command.Username}' is already taken."));

        // Update username, role, and optionally password via domain methods
        user.UpdateUsername(command.Username);
        user.UpdateRole(command.Role);

        if (!string.IsNullOrWhiteSpace(command.NewPassword))
        {
            var newHash = _passwordHasher.HashPassword(command.NewPassword);
            user.UpdatePassword(newHash);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Admin user updated successfully.");
    }
}
