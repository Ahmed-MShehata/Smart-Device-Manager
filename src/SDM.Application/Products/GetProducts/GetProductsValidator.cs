using FluentValidation;

namespace SDM.Application.Products.GetProducts;

/// <summary>
/// FluentValidation validator for <see cref="GetProductsQuery"/>.
/// Ensures pagination parameters are within acceptable ranges before the query executes.
/// Invoked automatically by <c>ValidationBehavior</c> in the MediatR pipeline.
/// </summary>
public sealed class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    /// <summary>Initializes a new instance of <see cref="GetProductsValidator"/> and defines all rules.</summary>
    public GetProductsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");

        RuleFor(x => x.SortBy)
            .IsInEnum()
                .WithMessage("Invalid sort field. Valid values: Name, Price, CreatedAt.");

        RuleFor(x => x.SortDirection)
            .IsInEnum()
                .WithMessage("Invalid sort direction. Valid values: Ascending, Descending.");
    }
}
