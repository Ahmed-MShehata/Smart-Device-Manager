using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Products.UploadProductImage;

/// <summary>
/// Handles <see cref="UploadProductImageCommand"/>.
/// Uploads a new image (or replaces an existing one) for a product.
/// </summary>
/// <remarks>
/// <para>
/// The sequence of operations is:
/// <list type="number">
///   <item><description>Locate the product. Return 404 if not found.</description></item>
///   <item><description>Save the new image file. Failure here leaves the DB untouched.</description></item>
///   <item><description>Update <c>Product.ImagePath</c> in the database.</description></item>
///   <item><description>Delete the previous image file (if one existed) after the DB commit.</description></item>
/// </list>
/// </para>
/// <para>
/// Step 4 is intentionally performed <em>after</em> the DB save so that a failed
/// database update never orphans a new file. If the old-file deletion fails, it is
/// silently ignored — storage can always be reclaimed during a maintenance sweep.
/// </para>
/// </remarks>
public sealed class UploadProductImageHandler
    : ICommandHandler<UploadProductImageCommand, UploadProductImageResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorage;

    /// <summary>
    /// Initializes a new instance of <see cref="UploadProductImageHandler"/>.
    /// </summary>
    public UploadProductImageHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorage)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    /// <inheritdoc/>
    public async Task<Result<UploadProductImageResponse>> Handle(
        UploadProductImageCommand command,
        CancellationToken cancellationToken)
    {
        // 1. Locate product
        var product = await _unitOfWork.Products.GetByIdAsync(command.ProductId, cancellationToken);
        if (product is null)
            return Result<UploadProductImageResponse>.Failure(Error.NotFound("Product"));

        // Capture old image path before overwriting (may be null)
        var previousImagePath = product.ImagePath;

        // 2. Save new image file first — if storage fails, the DB is not touched
        var relativePath = await _fileStorage.SaveImageAsync(
            command.FileStream, command.ContentType, cancellationToken);

        // 3. Update domain entity and persist
        product.SetImagePath(relativePath);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Delete the old image only after a successful DB commit
        if (!string.IsNullOrWhiteSpace(previousImagePath))
        {
            await _fileStorage.DeleteAsync(previousImagePath, cancellationToken);
        }

        return Result<UploadProductImageResponse>.Success(new UploadProductImageResponse
        {
            ProductId  = product.Id,
            ImagePath  = relativePath
        });
    }
}
