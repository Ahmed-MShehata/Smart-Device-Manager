using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.Products.DeleteProduct;

/// <summary>
/// Handles the <see cref="DeleteProductCommand"/>.
/// Soft-deletes the product by setting its status to <see cref="ProductStatus.Inactive"/>
/// using the domain's own <c>SetStatus</c> method.
/// The product record is retained in the database.
/// No new delete mechanism is introduced — the existing domain behavior is reused.
/// </summary>
public sealed class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteProductHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">Unit of work for coordinated persistence.</param>
    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
            return Result.Failure(Error.NotFound(nameof(product)));

        if (product.Status == ProductStatus.Inactive)
            return Result.Failure(Error.Conflict(nameof(product)));

        product.SetStatus(ProductStatus.Inactive);

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Product deactivated successfully.");
    }
}
