using FluentValidation;
using SDM.Domain.Enums;

namespace SDM.Application.AdminUsers.UpdateAdminUser;

/// <summary>Validates <see cref="UpdateAdminUserCommand"/>.</summary>
public sealed class UpdateAdminUserValidator : AbstractValidator<UpdateAdminUserCommand>
{
    public UpdateAdminUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Admin user ID is required.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid admin role specified.");

        RuleFor(x => x.NewPassword)
            .MinimumLength(6).WithMessage("New password must be at least 6 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.NewPassword));
    }
}
