namespace SDM.Application.Products.UploadProductImage;

/// <summary>
/// DTO returned after a product image has been successfully uploaded or replaced.
/// </summary>
public sealed class UploadProductImageResponse
{
    /// <summary>Gets the unique identifier of the product whose image was updated.</summary>
    public Guid ProductId { get; init; }

    /// <summary>
    /// Gets the relative path of the stored image.
    /// Suitable for constructing a public URL by prepending the API base address.
    /// Example: <c>images/products/3f2a1b.jpg</c>.
    /// </summary>
    public string ImagePath { get; init; } = string.Empty;
}
