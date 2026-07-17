using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Products.CreateProduct;
using SDM.Application.Products.DeleteProduct;
using SDM.Application.Products.DeleteProductImage;
using SDM.Application.Products.GetProduct;
using SDM.Application.Products.GetProducts;
using SDM.Application.Products.UpdateProduct;
using SDM.Application.Products.UploadProductImage;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all incoming client HTTP requests for hardware products.
/// Translates routes into MediatR commands and queries, enforcing policy-based security.
/// </summary>
public sealed class ProductsController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated, filtered, and sorted catalog of products.
    /// Accessible anonymously by customers and administrators.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetProductsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves full detailed specifications for a single product by ID.
    /// Accessible anonymously.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetProduct))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProduct([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetProductQuery { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Registers a new product in the database.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetProduct),
                new { id = result.Data.Id },
                new ApiResponse<CreateProductResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Replaces the properties of an existing product.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The product ID specified in the URL path does not match the ID in the request body.",
                null,
                [new ApiError("Product.IdMismatch", "URL ID and body ID must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Performs a soft delete by marking the product's availability status as Inactive.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProduct([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteProductCommand { Id = id });
        return HandleResult(result);
    }

    // ─── Image Endpoints ──────────────────────────────────────────────────────

    /// <summary>
    /// Uploads or replaces the image for a product.
    /// If the product already has an image, the old file is deleted after the new one is saved.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    /// <remarks>
    /// Send as <c>multipart/form-data</c>. The file field name must be <c>image</c>.
    /// Allowed types: JPEG, PNG, WebP. Maximum size: 5 MB.
    /// </remarks>
    [HttpPut("{id:guid}/image")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<UploadProductImageResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UploadProductImage([FromRoute] Guid id, IFormFile image)
    {
        if (image is null || image.Length == 0)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "No image file was provided.",
                null,
                [new ApiError("Image.Missing", "A valid image file is required.")]));
        }

        await using var fileStream = image.OpenReadStream();

        var command = new UploadProductImageCommand
        {
            ProductId   = id,
            FileStream  = fileStream,
            FileName    = image.FileName,
            ContentType = image.ContentType,
            FileSize    = image.Length
        };

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Removes the image from a product and deletes the physical file.
    /// Returns an error if the product has no image.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("{id:guid}/image")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProductImage([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteProductImageCommand { ProductId = id });
        return HandleResult(result);
    }
}
