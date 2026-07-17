using SDM.Application.Common.CQRS;

namespace SDM.Application.Products.DeleteProduct;

/// <summary>
/// Command that deactivates a product by setting its status to
/// <see cref="Domain.Enums.ProductStatus.Inactive"/>.
/// This is a soft delete — the product record is preserved in the database
/// but hidden from customers.
/// Returns a non-generic <see cref="Common.Result"/> indicating success or failure.
/// </summary>
public sealed class DeleteProductCommand : ICommand
{
    /// <summary>Gets the unique identifier of the product to deactivate.</summary>
    public Guid Id { get; init; }
}
