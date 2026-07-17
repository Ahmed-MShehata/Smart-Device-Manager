using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Products.GetProduct;

/// <summary>
/// Handles the <see cref="GetProductQuery"/>.
/// Retrieves a single product by ID and projects it to a <see cref="GetProductResponse"/> DTO.
/// Returns <see cref="Error.NotFound"/> if the product does not exist.
/// </summary>
public sealed class GetProductHandler : IQueryHandler<GetProductQuery, GetProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of <see cref="GetProductHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">Unit of work providing read access to the product repository.</param>
    public GetProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result<GetProductResponse>> Handle(
        GetProductQuery query,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(query.Id, cancellationToken);

        if (product is null)
            return Result<GetProductResponse>.Failure(Error.NotFound(nameof(Product)));

        return Result<GetProductResponse>.Success(MapToResponse(product));
    }

    /// <summary>
    /// Projects a <see cref="Product"/> entity to a <see cref="GetProductResponse"/> DTO.
    /// Manual mapping — no AutoMapper.
    /// </summary>
    private static GetProductResponse MapToResponse(Product product) => new()
    {
        Id             = product.Id,
        Name           = product.Name,
        Brand          = product.Brand,
        Description    = product.Description,
        Category       = product.Category,
        Price          = product.Price,
        Discount       = product.Discount,
        FinalPrice     = product.FinalPrice,
        Quantity       = product.Quantity,
        WarrantyMonths = product.WarrantyMonths,
        ImagePath      = product.ImagePath,
        Status         = product.Status,
        CreatedAt      = product.CreatedAt,
        CreatedBy      = product.CreatedBy
    };
}
