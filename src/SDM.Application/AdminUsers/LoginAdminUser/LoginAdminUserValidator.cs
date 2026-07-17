using FluentValidation;

namespace SDM.Application.AdminUsers.LoginAdminUser;

/// <summary>
/// Validates <see cref="LoginAdminUserCommand"/>.
/// </summary>
public sealed class LoginAdminUserValidator : AbstractValidator<LoginAdminUserCommand>
{
    /// <summary>Initializes a new instance of <see cref="LoginAdminUserValidator"/>.</summary>
    public LoginAdminUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
