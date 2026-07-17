namespace SDM.Application.Products.GetProducts;

/// <summary>
/// Defines the direction of a sort operation.
/// Used in <see cref="GetProductsQuery.SortDirection"/>.
/// </summary>
public enum SortDirection
{
    /// <summary>Sort from smallest to largest (A→Z, 0→9, oldest→newest).</summary>
    Ascending = 0,

    /// <summary>Sort from largest to smallest (Z→A, 9→0, newest→oldest).</summary>
    Descending = 1
}
