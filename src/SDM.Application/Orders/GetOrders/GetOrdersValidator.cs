using FluentValidation;

namespace SDM.Application.Orders.GetOrders;

/// <summary>
/// FluentValidation validator for <see cref="GetOrdersQuery"/>.
/// Enforces pagination bounds. All other parameters are optional.
/// Invoked automatically by <c>ValidationBehavior</c> in the MediatR pipeline.
/// </summary>
public sealed class GetOrdersValidator : AbstractValidator<GetOrdersQuery>
{
    /// <summary>
    /// Initializes a new instance of <see cref="GetOrdersValidator"/>
    /// and defines all validation rules.
    /// </summary>
    public GetOrdersValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100.");
    }
}
