using FluentValidation;

namespace SDM.Application.AdminUsers.CreateAdminUser;

/// <summary>
/// Validates <see cref="CreateAdminUserCommand"/>.
/// </summary>
public sealed class CreateAdminUserValidator : AbstractValidator<CreateAdminUserCommand>
{
    /// <summary>Initializes a new instance of <see cref="CreateAdminUserValidator"/>.</summary>
    public CreateAdminUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .Matches("^[a-zA-Z0-9_.-]+$").WithMessage("Username may only contain letters, digits, underscores, dots, and hyphens.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .MaximumLength(128).WithMessage("Password must not exceed 128 characters.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("A valid role must be selected.");
    }
}
