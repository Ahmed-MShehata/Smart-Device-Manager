using SDM.Domain.Enums;

namespace SDM.Application.Products.GetProduct;

/// <summary>
/// DTO returned by <see cref="GetProductHandler"/> for a single product.
/// Contains the full product detail required by the client.
/// Does not expose domain entities or EF Core types.
/// </summary>
public sealed class GetProductResponse
{
    /// <summary>Gets the unique identifier of the product.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name of the product.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the manufacturer brand.</summary>
    public string Brand { get; init; } = string.Empty;

    /// <summary>Gets the full product description.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the hardware category of the product.</summary>
    public ProductCategory Category { get; init; }

    /// <summary>Gets the base retail price before discount.</summary>
    public decimal Price { get; init; }

    /// <summary>Gets the discount percentage applied. Range: 0–100.</summary>
    public decimal Discount { get; init; }

    /// <summary>Gets the final price after discount. Computed: Price − (Price × Discount / 100).</summary>
    public decimal FinalPrice { get; init; }

    /// <summary>Gets the current stock quantity.</summary>
    public int Quantity { get; init; }

    /// <summary>Gets the warranty duration in months. Zero means no warranty.</summary>
    public int WarrantyMonths { get; init; }

    /// <summary>Gets the path or URL to the product image. Null if not set.</summary>
    public string? ImagePath { get; init; }

    /// <summary>Gets the current availability status.</summary>
    public ProductStatus Status { get; init; }

    /// <summary>Gets the UTC timestamp when the product was created.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>Gets the username of the admin who created the product.</summary>
    public string CreatedBy { get; init; } = string.Empty;
}
