using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.Orders.UpdateOrderStatus;

/// <summary>
/// Handles <see cref="UpdateOrderStatusCommand"/>.
/// Validates that the order can still be modified, delegates the transition
/// to the domain <c>Order.UpdateStatus</c> method, and optionally updates admin notes.
/// </summary>
/// <remarks>
/// Terminal orders (Delivered, Cancelled) are rejected before the domain method is invoked
/// to return a clean domain <see cref="Error"/> instead of propagating an exception.
/// </remarks>
public sealed class UpdateOrderStatusHandler : ICommandHandler<UpdateOrderStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="UpdateOrderStatusHandler"/>.</summary>
    public UpdateOrderStatusHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        UpdateOrderStatusCommand command,
        CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(command.OrderId, cancellationToken);
        if (order is null)
            return Result.Failure(Error.NotFound("Order"));

        // Guard terminal states before delegating to the domain method
        if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
            return Result.Failure(new Error(
                "Order.TerminalState",
                $"Cannot modify an order that is already {order.Status}."));

        order.UpdateStatus(command.NewStatus);

        if (command.Notes is not null)
            order.SetNotes(command.Notes);

        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success($"Order status updated to {command.NewStatus}.");
    }
}
