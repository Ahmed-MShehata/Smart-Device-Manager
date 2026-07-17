using FluentValidation;

namespace SDM.Application.SystemComponents.AttachComponentFile;

/// <summary>
/// Validates <see cref="AttachComponentFileCommand"/>.
/// </summary>
public sealed class AttachComponentFileValidator : AbstractValidator<AttachComponentFileCommand>
{
    /// <summary>Initializes a new instance of <see cref="AttachComponentFileValidator"/>.</summary>
    public AttachComponentFileValidator()
    {
        RuleFor(x => x.ComponentId).NotEmpty().WithMessage("Component ID is required.");

        RuleFor(x => x.FileType).IsInEnum().WithMessage("A valid file type is required.");

        RuleFor(x => x.StoredFileName)
            .NotEmpty().WithMessage("Stored file name is required.")
            .MaximumLength(200).WithMessage("Stored file name must not exceed 200 characters.")
            .Must(n => !n.Contains('/') && !n.Contains('\\')).WithMessage("Stored file name must not contain path separators.");

        RuleFor(x => x.OriginalFileName)
            .NotEmpty().WithMessage("Original file name is required.")
            .MaximumLength(500).WithMessage("Original file name must not exceed 500 characters.");

        RuleFor(x => x.RelativePath)
            .NotEmpty().WithMessage("Relative path is required.")
            .MaximumLength(1000).WithMessage("Relative path must not exceed 1000 characters.")
            .Must(p => !Path.IsPathRooted(p)).WithMessage("Absolute paths are not allowed. Supply a relative path only.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("File size must be greater than zero.");

        RuleFor(x => x.MimeType)
            .NotEmpty().WithMessage("MIME type is required.")
            .MaximumLength(200).WithMessage("MIME type must not exceed 200 characters.");

        RuleFor(x => x.SHA256)
            .NotEmpty().WithMessage("SHA-256 hash is required.")
            .Length(64).WithMessage("SHA-256 hash must be exactly 64 characters.")
            .Matches("^[0-9a-fA-F]{64}$").WithMessage("SHA-256 must be a valid hex string.");

        RuleFor(x => x.Version)
            .MaximumLength(50).WithMessage("Version must not exceed 50 characters.")
            .When(x => x.Version is not null);
    }
}
