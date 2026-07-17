namespace SDM.Application.Common;

/// <summary>
/// Represents the outcome of an operation that produces no value.
/// Encapsulates success/failure state along with an optional message and error list.
/// </summary>
public class Result : IResultFactory<Result>
{
    /// <summary>Gets a value indicating whether the operation succeeded.</summary>
    public bool IsSuccess { get; }

    /// <summary>Gets a value indicating whether the operation failed.</summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>Gets the human-readable message describing the outcome.</summary>
    public string Message { get; }

    /// <summary>Gets the list of errors associated with this result. Empty on success.</summary>
    public IReadOnlyList<Error> Errors { get; }

    /// <summary>Initializes a new <see cref="Result"/>.</summary>
    protected Result(bool isSuccess, string message, IReadOnlyList<Error> errors)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors;
    }

    // ─── Factory Methods ──────────────────────────────────────────────────

    /// <summary>Creates a successful result with an optional message.</summary>
    public static Result Success(string message = "Operation completed successfully.")
        => new(true, message, []);

    /// <summary>Creates a failed result from a single error.</summary>
    public static Result Failure(Error error)
        => new(false, error.Description, [error]);

    /// <summary>Creates a failed result from multiple errors.</summary>
    public static Result Failure(IReadOnlyList<Error> errors, string message = "One or more errors occurred.")
        => new(false, message, errors);

    /// <summary>Creates a failed result from a list of validation error descriptions.</summary>
    public static Result Failure(string message)
        => new(false, message, [new Error("General.Error", message)]);

    /// <summary>Creates a typed result from this non-typed result. Used for implicit conversion scenarios.</summary>
    public static Result<T> Success<T>(T data, string message = "Operation completed successfully.")
        => Result<T>.Success(data, message);

    /// <summary>Creates a typed failed result.</summary>
    public static Result<T> Failure<T>(Error error)
        => Result<T>.Failure(error);
}
