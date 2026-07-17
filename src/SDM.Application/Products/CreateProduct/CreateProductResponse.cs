using SDM.Domain.Enums;

namespace SDM.Application.Products.CreateProduct;

/// <summary>
/// DTO returned to the caller after a product is successfully created.
/// Contains only the data the client needs to confirm the operation and display the result.
/// Does not expose domain entities or EF Core types.
/// </summary>
public sealed class CreateProductResponse
{
    /// <summary>Gets the unique identifier assigned to the newly created product.</summary>
    public Guid Id { get; init; }

    /// <summary>Gets the display name of the product.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the manufacturer brand.</summary>
    public string Brand { get; init; } = string.Empty;

    /// <summary>Gets the hardware category of the product.</summary>
    public ProductCategory Category { get; init; }

    /// <summary>Gets the base retail price before discount.</summary>
    public decimal Price { get; init; }

    /// <summary>Gets the discount percentage applied. Range: 0–100.</summary>
    public decimal Discount { get; init; }

    /// <summary>
    /// Gets the final price after applying the discount.
    /// Computed: Price − (Price × Discount / 100).
    /// </summary>
    public decimal FinalPrice { get; init; }

    /// <summary>Gets the initial stock quantity.</summary>
    public int Quantity { get; init; }

    /// <summary>Gets the warranty duration in months. Zero means no warranty.</summary>
    public int WarrantyMonths { get; init; }

    /// <summary>Gets the path or URL to the product image. Null if not provided.</summary>
    public string? ImagePath { get; init; }

    /// <summary>Gets the UTC timestamp when the product was created.</summary>
    public DateTime CreatedAt { get; init; }
}
