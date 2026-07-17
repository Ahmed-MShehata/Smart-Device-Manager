using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Products.CreateProduct;

/// <summary>
/// Handles the <see cref="CreateProductCommand"/>.
/// Creates a new <see cref="Product"/> domain entity, persists it through the
/// Unit of Work, and returns a <see cref="CreateProductResponse"/> on success.
/// Audit fields (<c>CreatedBy</c>, <c>CreatedAt</c>) are stamped automatically
/// by the Infrastructure layer during save — the handler does not set them.
/// </summary>
/// <remarks>
/// Validation is performed upstream by <c>ValidationBehavior</c> in the MediatR pipeline.
/// This handler assumes all inputs are already validated when it executes.
/// </remarks>
public sealed class CreateProductHandler
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateProductHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">Unit of work for coordinated persistence.</param>
    public CreateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result<CreateProductResponse>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = new Product(
            command.Name,
            command.Category,
            command.Brand,
            command.Description,
            command.Price,
            command.Discount,
            command.Quantity,
            command.WarrantyMonths,
            command.ImagePath);

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateProductResponse>.Success(
            MapToResponse(product),
            "Product created successfully.");
    }

    /// <summary>
    /// Maps a <see cref="Product"/> domain entity to a <see cref="CreateProductResponse"/> DTO.
    /// Manual mapping — no AutoMapper.
    /// </summary>
    private static CreateProductResponse MapToResponse(Product product) => new()
    {
        Id             = product.Id,
        Name           = product.Name,
        Brand          = product.Brand,
        Category       = product.Category,
        Price          = product.Price,
        Discount       = product.Discount,
        FinalPrice     = product.FinalPrice,
        Quantity       = product.Quantity,
        WarrantyMonths = product.WarrantyMonths,
        ImagePath      = product.ImagePath,
        CreatedAt      = product.CreatedAt
    };
}
