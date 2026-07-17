using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Products.DeleteProductImage;

/// <summary>
/// Handles <see cref="DeleteProductImageCommand"/>.
/// Clears the product's <c>ImagePath</c> in the database and deletes the physical file.
/// </summary>
/// <remarks>
/// The database update is committed before the physical file is deleted.
/// If the file deletion fails silently the record in the database is already correct,
/// and the orphaned file can be reclaimed during a maintenance sweep.
/// </remarks>
public sealed class DeleteProductImageHandler : ICommandHandler<DeleteProductImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorage;

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteProductImageHandler"/>.
    /// </summary>
    public DeleteProductImageHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorage)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(
        DeleteProductImageCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(command.ProductId, cancellationToken);
        if (product is null)
            return Result.Failure(Error.NotFound("Product"));

        if (string.IsNullOrWhiteSpace(product.ImagePath))
            return Result.Failure(
                new Error("Product.NoImage", "This product does not have an image to delete."));

        var imagePath = product.ImagePath;

        // Clear DB record first — if file deletion fails the DB is still consistent
        product.SetImagePath(null);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Delete physical file (silently tolerates missing files)
        await _fileStorage.DeleteAsync(imagePath, cancellationToken);

        return Result.Success("Product image deleted successfully.");
    }
}
