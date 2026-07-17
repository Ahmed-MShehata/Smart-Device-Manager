using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Enums;

namespace SDM.Application.Orders.CancelOrder;

/// <summary>
/// Handles <see cref="CancelOrderCommand"/>.
/// Transitions the order to <see cref="OrderStatus.Cancelled"/> (soft cancel — data is preserved).
/// Returns an error if the order is already in a terminal state.
/// </summary>
public sealed class CancelOrderHandler : ICommandHandler<CancelOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="CancelOrderHandler"/>.</summary>
    public CancelOrderHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> Handle(
        CancelOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(command.OrderId, cancellationToken);
        if (order is null)
            return Result.Failure(Error.NotFound("Order"));

        if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
            return Result.Failure(new Error(
                "Order.TerminalState",
                $"Cannot cancel an order that is already {order.Status}."));

        order.UpdateStatus(OrderStatus.Cancelled);

        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Order cancelled successfully.");
    }
}
