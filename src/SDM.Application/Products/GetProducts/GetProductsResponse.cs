using SDM.Domain.Enums;

namespace SDM.Application.Products.GetProducts;

/// <summary>
/// DTO representing a single product row in a paginated product list.
/// Contains summary-level data suitable for catalog or admin list views.
/// Projected directly from the database — no entity is loaded into memory.
/// </summary>
public sealed class GetProductsResponse
{
    /// <summary>Gets the unique identifier of the product.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name of the product.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the manufacturer brand.</summary>
    public string Brand { get; init; } = string.Empty;

    /// <summary>Gets the hardware category of the product.</summary>
    public ProductCategory Category { get; init; }

    /// <summary>Gets the base retail price before discount.</summary>
    public decimal Price { get; init; }

    /// <summary>Gets the discount percentage. Range: 0–100.</summary>
    public decimal Discount { get; init; }

    /// <summary>
    /// Gets the final price after applying the discount.
    /// Computed server-side: Price − (Price × Discount / 100).
    /// </summary>
    public decimal FinalPrice { get; init; }

    /// <summary>Gets the current stock quantity.</summary>
    public int Quantity { get; init; }

    /// <summary>Gets the path or URL to the product image. Null if not set.</summary>
    public string? ImagePath { get; init; }

    /// <summary>Gets the current availability status of the product.</summary>
    public ProductStatus Status { get; init; }
}
