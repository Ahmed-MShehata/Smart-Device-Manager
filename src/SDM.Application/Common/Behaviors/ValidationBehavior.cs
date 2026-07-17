using FluentValidation;
using MediatR;
using SDM.Application.Common;

namespace SDM.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that automatically validates every command and query
/// before it reaches its handler.
/// </summary>
/// <remarks>
/// <para>
/// Intercepts every <see cref="IRequest{TResponse}"/> dispatched through MediatR and
/// runs all registered <see cref="IValidator{T}"/> implementations for that request type.
/// </para>
/// <para>
/// If validation fails the handler is <b>never invoked</b>. A failure result is
/// returned immediately by calling <c>TResponse.Failure(errors, message)</c> —
/// a compile-time call resolved through the <see cref="IResultFactory{TSelf}"/> constraint.
/// No reflection or runtime type inspection is used.
/// </para>
/// <para>
/// If no validators are registered for a request type the pipeline passes through
/// with zero overhead.
/// </para>
/// </remarks>
/// <typeparam name="TRequest">The request type (command or query).</typeparam>
/// <typeparam name="TResponse">
/// The response type. Constrained to <see cref="IResultFactory{TSelf}"/>, which is
/// satisfied by both <see cref="Result"/> and <see cref="Result{T}"/>.
/// </typeparam>
public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResultFactory<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of <see cref="ValidationBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="validators">
    /// All validators registered for <typeparamref name="TRequest"/>.
    /// Injected automatically by the DI container.
    /// An empty collection is valid — validation is skipped.
    /// </param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Fast path: no validators registered for this request type
        if (!_validators.Any())
            return await next();

        // Run all validators concurrently
        var validationTasks = _validators
            .Select(v => v.ValidateAsync(request, cancellationToken));

        var results = await Task.WhenAll(validationTasks);

        // Collect all failures across all validators
        var failures = results
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        // No failures — proceed to the handler
        if (failures.Count == 0)
            return await next();

        // Map FluentValidation failures to domain Error objects
        var errors = failures
            .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
            .ToList();

        // Compile-time call through IResultFactory<TResponse>.
        // No reflection. No runtime casting. No dynamic dispatch.
        // Works for both Result and Result<T> via static abstract interface member.
        return TResponse.Failure(errors, "Validation failed.");
    }
}
