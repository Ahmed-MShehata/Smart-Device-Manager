using FluentValidation;

namespace SDM.Application.Products.UploadProductImage;

/// <summary>
/// FluentValidation validator for <see cref="UploadProductImageCommand"/>.
/// Enforces file type allowlist and maximum size before the handler is invoked.
/// Invoked automatically by <c>ValidationBehavior</c> in the MediatR pipeline.
/// </summary>
public sealed class UploadProductImageValidator : AbstractValidator<UploadProductImageCommand>
{
    /// <summary>Allowed MIME types for product images.</summary>
    private static readonly HashSet<string> AllowedContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/webp"
    ];

    /// <summary>Maximum allowed file size: 5 MB.</summary>
    private const long MaxFileSizeBytes = 5L * 1024 * 1024;

    /// <summary>
    /// Initializes a new instance of <see cref="UploadProductImageValidator"/>
    /// and defines all validation rules.
    /// </summary>
    public UploadProductImageValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required.");

        RuleFor(x => x.ContentType)
            .Must(ct => AllowedContentTypes.Contains(ct))
            .WithMessage("Only JPEG, PNG, and WebP images are accepted.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0)
            .WithMessage("File must not be empty.")
            .LessThanOrEqualTo(MaxFileSizeBytes)
            .WithMessage("Image must not exceed 5 MB.");
    }
}
