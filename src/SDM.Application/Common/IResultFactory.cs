namespace SDM.Application.Common;

/// <summary>
/// Marks a result type as capable of constructing a failure instance from a list of errors.
/// Implemented by <see cref="Result"/> and <see cref="Result{T}"/> to enable
/// compile-time type-safe failure creation inside pipeline behaviors —
/// without reflection or runtime type inspection.
/// </summary>
/// <remarks>
/// Uses the C# 11 static abstract interface member feature.
/// The constraint <c>where TResponse : IResultFactory&lt;TResponse&gt;</c> on
/// <c>ValidationBehavior&lt;TRequest, TResponse&gt;</c> allows the behavior to call
/// <c>TResponse.Failure(errors, message)</c> directly with full compile-time safety.
/// </remarks>
/// <typeparam name="TSelf">
/// The concrete result type. Must be the same type that implements this interface
/// (curiously recurring template pattern).
/// </typeparam>
public interface IResultFactory<TSelf> where TSelf : IResultFactory<TSelf>
{
    /// <summary>
    /// Creates a failed result from a list of <see cref="Error"/> objects and a summary message.
    /// </summary>
    /// <param name="errors">The list of errors that caused the failure. Must not be empty.</param>
    /// <param name="message">A human-readable summary of the failure.</param>
    /// <returns>A failed <typeparamref name="TSelf"/> instance carrying the provided errors.</returns>
    static abstract TSelf Failure(IReadOnlyList<Error> errors, string message);
}
