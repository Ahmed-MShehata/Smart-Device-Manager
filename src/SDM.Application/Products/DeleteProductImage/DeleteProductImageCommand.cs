using SDM.Application.Common.CQRS;

namespace SDM.Application.Products.DeleteProductImage;

/// <summary>
/// Command that removes the image from a product and deletes the physical file.
/// Handled by <see cref="DeleteProductImageHandler"/>.
/// Returns a non-generic <see cref="Common.Result"/> indicating success or failure.
/// </summary>
public sealed class DeleteProductImageCommand : ICommand
{
    /// <summary>Gets the unique identifier of the product whose image should be removed.</summary>
    public Guid ProductId { get; init; }
}
