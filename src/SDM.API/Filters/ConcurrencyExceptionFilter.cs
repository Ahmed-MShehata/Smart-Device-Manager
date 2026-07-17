using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace SDM.API.Filters;

/// <summary>
/// Global exception filter that intercepts <see cref="DbUpdateConcurrencyException"/> thrown
/// by any write handler and converts it into a standardised HTTP 409 Conflict response.
/// This keeps the CQRS handlers free of infrastructure-level exception handling.
/// </summary>
public sealed class ConcurrencyExceptionFilter : IExceptionFilter
{
    /// <inheritdoc/>
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not DbUpdateConcurrencyException)
            return;

        var response = new
        {
            Success = false,
            Message = "The record you attempted to modify was updated by another user. Please reload and try again.",
            Data    = (object?)null,
            Errors  = new[]
            {
                new { Code = "Concurrency.Conflict", Description = context.Exception.Message }
            }
        };

        context.Result            = new ConflictObjectResult(response);
        context.ExceptionHandled  = true;
    }
}
