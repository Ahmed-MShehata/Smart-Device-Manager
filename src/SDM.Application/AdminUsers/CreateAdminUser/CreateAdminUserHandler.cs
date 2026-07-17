using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.AdminUsers.CreateAdminUser;

/// <summary>
/// Handles <see cref="CreateAdminUserCommand"/>.
/// Enforces username uniqueness and hashes the password before persisting.
/// </summary>
public sealed class CreateAdminUserHandler : ICommandHandler<CreateAdminUserCommand, CreateAdminUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _db;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>Initializes a new instance of <see cref="CreateAdminUserHandler"/>.</summary>
    public CreateAdminUserHandler(IUnitOfWork unitOfWork, IReadDbContext db, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _db = db;
        _passwordHasher = passwordHasher;
    }

    /// <inheritdoc/>
    public async Task<Result<CreateAdminUserResponse>> Handle(CreateAdminUserCommand command, CancellationToken cancellationToken)
    {
        // Enforce username uniqueness
        var usernameExists = await _db.AdminUsers
            .AnyAsync(u => u.Username == command.Username, cancellationToken);

        if (usernameExists)
            return Result<CreateAdminUserResponse>.Failure(
                Error.Conflict("AdminUser", $"Username '{command.Username}' is already taken."));

        var passwordHash = _passwordHasher.HashPassword(command.Password);
        var adminUser = new AdminUser(command.Username, passwordHash, command.Role);

        await _unitOfWork.AdminUsers.AddAsync(adminUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateAdminUserResponse>.Success(
            new CreateAdminUserResponse
            {
                Id       = adminUser.Id,
                Username = adminUser.Username,
                Role     = adminUser.Role
            },
            "Admin user created successfully.");
    }
}
