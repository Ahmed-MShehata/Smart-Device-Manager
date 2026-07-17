namespace SDM.Application.Products.GetProducts;

/// <summary>
/// Defines the fields by which a product list can be sorted.
/// Used in <see cref="GetProductsQuery.SortBy"/>.
/// </summary>
public enum ProductSortBy
{
    /// <summary>Sort alphabetically by product name.</summary>
    Name = 0,

    /// <summary>Sort numerically by base retail price.</summary>
    Price = 1,

    /// <summary>Sort chronologically by the date the product was created.</summary>
    CreatedAt = 2
}
