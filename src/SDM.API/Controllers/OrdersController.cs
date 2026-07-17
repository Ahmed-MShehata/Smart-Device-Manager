using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDM.API.Core;
using SDM.Application.Common;
using SDM.Application.Orders.CancelOrder;
using SDM.Application.Orders.CreateOrder;
using SDM.Application.Orders.GetOrder;
using SDM.Application.Orders.GetOrders;
using SDM.Application.Orders.UpdateOrderStatus;

namespace SDM.API.Controllers;

/// <summary>
/// Handles all HTTP requests for customer orders.
/// POST (create) is anonymous — all other operations require admin authentication.
/// </summary>
public sealed class OrdersController : ApiControllerBase
{
    /// <summary>
    /// Places a new customer order.
    /// Validates product availability and stock before persisting.
    /// Accessible anonymously — intended for customer desktop applications.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CreateOrderResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.IsSuccess)
        {
            return CreatedAtRoute(
                nameof(GetOrder),
                new { id = result.Data.Id },
                new ApiResponse<CreateOrderResponse>(true, result.Message, result.Data, []));
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves the full details of a single order, including all line items.
    /// Restricted to any authenticated admin.
    /// </summary>
    [HttpGet("{id:guid}", Name = nameof(GetOrder))]
    [Authorize(Policy = Policies.RequireAnyAdmin)]
    [ProducesResponseType(typeof(ApiResponse<GetOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetOrder([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new GetOrderQuery { Id = id });
        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves a paginated, filtered, and sorted list of orders.
    /// Supports search by customer name or phone, filter by status, and multiple sort options.
    /// Restricted to any authenticated admin.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = Policies.RequireAnyAdmin)]
    [ProducesResponseType(typeof(ApiResponse<PaginationResponse<GetOrdersResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> GetOrders([FromQuery] GetOrdersQuery query)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result);
    }

    /// <summary>
    /// Updates the lifecycle status of an order and optionally sets admin notes.
    /// Terminals orders (Delivered, Cancelled) cannot be modified.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateOrderStatus(
        [FromRoute] Guid id,
        [FromBody] UpdateOrderStatusCommand command)
    {
        if (id != command.OrderId)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "The order ID in the URL does not match the ID in the request body.",
                null,
                [new ApiError("Order.IdMismatch", "URL ID and body OrderId must match.")]));
        }

        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    /// <summary>
    /// Cancels an order (soft cancel — data is preserved).
    /// Terminal orders (Delivered, Cancelled) cannot be cancelled again.
    /// Restricted to SuperAdmins and Admins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.RequireAdmin)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelOrder([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new CancelOrderCommand { OrderId = id });
        return HandleResult(result);
    }
}
