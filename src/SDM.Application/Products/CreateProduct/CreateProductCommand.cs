using SDM.Application.Common.CQRS;
using SDM.Domain.Enums;

namespace SDM.Application.Products.CreateProduct;

/// <summary>
/// Command that creates a new hardware product in the system.
/// Dispatched by the API layer through MediatR and handled by <see cref="CreateProductHandler"/>.
/// Returns a <see cref="CreateProductResponse"/> wrapped in a <see cref="Common.Result{T}"/>.
/// </summary>
public sealed class CreateProductCommand : ICommand<CreateProductResponse>
{
    /// <summary>Gets the display name of the product. Required. Max 200 characters.</summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>Gets the hardware category of the product.</summary>
    public ProductCategory Category { get; init; }

    /// <summary>Gets the manufacturer brand. Required. Max 100 characters.</summary>
    public string Brand { get; init; } = string.Empty;

    /// <summary>Gets the full product description. Required. Max 2000 characters.</summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>Gets the base retail price. Must be greater than zero.</summary>
    public decimal Price { get; init; }

    /// <summary>Gets the discount percentage. Range: 0–100. Default: 0.</summary>
    public decimal Discount { get; init; }

    /// <summary>Gets the initial stock quantity. Must be zero or greater.</summary>
    public int Quantity { get; init; }

    /// <summary>Gets the warranty duration in months. Zero means no warranty.</summary>
    public int WarrantyMonths { get; init; }

    /// <summary>Gets the optional path or URL to the product image. Max 500 characters.</summary>
    public string? ImagePath { get; init; }
}
