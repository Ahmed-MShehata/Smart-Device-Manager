using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Products.UpdateProduct;

/// <summary>
/// Command that updates an existing hardware product.
/// All specified properties replace the current values on the entity.
/// Returns a non-generic <see cref="Common.Result"/> indicating success or failure.
/// </summary>
public sealed class UpdateProductCommand : ICommand
{
    /// <summary>Gets the unique identifier of the product to update.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the new display name. Required. Max 200 characters.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the new hardware category.</summary>
    public ProductCategory Category { get; init; }

    /// <summary>Gets the new manufacturer brand. Required. Max 100 characters.</summary>
    public string Brand { get; init; } = string.Empty;

    /// <summary>Gets the new full description. Required. Max 2000 characters.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the new base retail price. Must be greater than zero.</summary>
    public decimal Price { get; init; }

    /// <summary>Gets the new discount percentage. Range: 0–100.</summary>
    public decimal Discount { get; init; }

    /// <summary>Gets the new stock quantity. Must be zero or greater.</summary>
    public int Quantity { get; init; }

    /// <summary>Gets the new warranty duration in months. Zero means no warranty.</summary>
    public int WarrantyMonths { get; init; }

    /// <summary>Gets the optional new image path. Max 500 characters. Null clears the image.</summary>
    public string? ImagePath { get; init; }
}
