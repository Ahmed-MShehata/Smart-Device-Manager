using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Products.UpdateProduct;

/// <summary>
/// Handles the <see cref="UpdateProductCommand"/>.
/// Loads the product from the repository, calls the domain update method,
/// and persists the changes through the Unit of Work.
/// Audit fields are stamped automatically by Infrastructure.
/// </summary>
public sealed class UpdateProductHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateProductHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">Unit of work for coordinated persistence.</param>
    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
            return Result.Failure(Error.NotFound(nameof(product)));

        product.Update(
            command.Name,
            command.Category,
            command.Brand,
            command.Description,
            command.Price,
            command.Discount,
            command.Quantity,
            command.WarrantyMonths,
            command.ImagePath);

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Product updated successfully.");
    }
}
