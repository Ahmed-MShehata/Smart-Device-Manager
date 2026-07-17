using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a hardware product available for customers to browse and order.
/// Audit fields (<c>CreatedBy</c>, <c>UpdatedBy</c>, <c>UpdatedAt</c>) are stamped
/// automatically by Infrastructure — never by the handler or by this entity.
/// </summary>
public class Product : AuditableEntity
{
    /// <summary>Gets the display name of the product.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Gets the hardware category of the product.</summary>
    public ProductCategory Category { get; private set; }

    /// <summary>Gets the manufacturer brand of the product.</summary>
    public string Brand { get; private set; } = string.Empty;

    /// <summary>Gets the full product description.</summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>Gets the base retail price. Must be greater than zero.</summary>
    public decimal Price { get; private set; }

    /// <summary>Gets the discount percentage applied to the price. Range: 0–100.</summary>
    public decimal Discount { get; private set; }

    /// <summary>Gets the number of units available in stock. Cannot be negative.</summary>
    public int Quantity { get; private set; }

    /// <summary>Gets the warranty duration in months. Zero means no warranty.</summary>
    public int WarrantyMonths { get; private set; }

    /// <summary>Gets the path or URL to the product image. Null if not set.</summary>
    public string? ImagePath { get; private set; }

    /// <summary>Gets the current availability status of this product.</summary>
    public ProductStatus Status { get; private set; } = ProductStatus.Active;

    /// <summary>
    /// Gets the actual price after applying the discount.
    /// Computed: Price − (Price × Discount / 100). Not persisted.
    /// </summary>
    public decimal FinalPrice => Price - (Price * Discount / 100m);

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected Product() { }

    /// <summary>
    /// Creates a new <see cref="Product"/>.
    /// Status defaults to <see cref="ProductStatus.Active"/>.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    /// <param name="name">Display name. Required.</param>
    /// <param name="category">Hardware category.</param>
    /// <param name="brand">Manufacturer brand. Required.</param>
    /// <param name="description">Full description. Required.</param>
    /// <param name="price">Base retail price. Must be greater than zero.</param>
    /// <param name="discount">Discount percentage. Range: 0–100.</param>
    /// <param name="quantity">Stock count. Must be zero or greater.</param>
    /// <param name="warrantyMonths">Warranty duration in months. Zero means no warranty.</param>
    /// <param name="imagePath">Optional path or URL to the product image.</param>
    public Product(
        string name,
        ProductCategory category,
        string brand,
        string description,
        decimal price,
        decimal discount,
        int quantity,
        int warrantyMonths,
        string? imagePath = null)
    {
        Name = name;
        Category = category;
        Brand = brand;
        Description = description;
        Price = price;
        Discount = discount;
        Quantity = quantity;
        WarrantyMonths = warrantyMonths;
        ImagePath = imagePath;
        Status = ProductStatus.Active;
    }

    /// <summary>Updates all editable properties of this product.</summary>
    public void Update(
        string name,
        ProductCategory category,
        string brand,
        string description,
        decimal price,
        decimal discount,
        int quantity,
        int warrantyMonths,
        string? imagePath)
    {
        Name = name;
        Category = category;
        Brand = brand;
        Description = description;
        Price = price;
        Discount = discount;
        Quantity = quantity;
        WarrantyMonths = warrantyMonths;
        ImagePath = imagePath;
    }

    /// <summary>Sets the availability status of this product.</summary>
    /// <param name="status">The new <see cref="ProductStatus"/>.</param>
    public void SetStatus(ProductStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Sets or replaces the product image path.
    /// Pass <see langword="null"/> to clear the image.
    /// </summary>
    /// <param name="imagePath">Relative path to the image file, or null to remove the image.</param>
    public void SetImagePath(string? imagePath)
    {
        ImagePath = imagePath;
    }

    /// <summary>
    /// Reduces stock by the given amount. Sets status to
    /// <see cref="ProductStatus.OutOfStock"/> when quantity reaches zero.
    /// </summary>
    /// <param name="amount">Number of units to reduce.</param>
    /// <exception cref="InvalidOperationException">Thrown when stock would go below zero.</exception>
    public void ReduceStock(int amount)
    {
        if (Quantity - amount < 0)
            throw new InvalidOperationException(
                $"Insufficient stock. Available: {Quantity}, Requested: {amount}.");

        Quantity -= amount;

        if (Quantity == 0 && Status == ProductStatus.Active)
            Status = ProductStatus.OutOfStock;
    }
}
