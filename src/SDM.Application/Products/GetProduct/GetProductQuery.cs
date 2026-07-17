using SDM.Application.Common.CQRS;

namespace SDM.Application.Products.GetProduct;

/// <summary>
/// Query that returns the full details of a single product by its unique identifier.
/// Returns <see cref="Common.Result{T}"/> wrapping a <see cref="GetProductResponse"/>.
/// </summary>
public sealed class GetProductQuery : IQuery<GetProductResponse>
{
    /// <summary>Gets the unique identifier of the product to retrieve.</summary>
    public Guid Id { get; init; }
}
