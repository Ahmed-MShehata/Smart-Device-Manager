namespace SDM.Application.Common;

/// <summary>
/// Represents the outcome of an operation that produces a value of type <typeparamref name="T"/>.
/// Encapsulates success/failure state, the result data, message, and any errors.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public sealed class Result<T> : Result, IResultFactory<Result<T>>
{
    private readonly T? _data;

    /// <summary>
    /// Gets the operation result data.
    /// Only valid when <see cref="Result.IsSuccess"/> is true.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when accessing data on a failed result.</exception>
    public T Data => IsSuccess
        ? _data!
        : throw new InvalidOperationException("Cannot access data of a failed result.");

    /// <summary>Initializes a new <see cref="Result{T}"/>.</summary>
    private Result(bool isSuccess, T? data, string message, IReadOnlyList<Error> errors)
        : base(isSuccess, message, errors)
    {
        _data = data;
    }

    // ─── Factory Methods ──────────────────────────────────────────────────

    /// <summary>Creates a successful typed result with data.</summary>
    public static Result<T> Success(T data, string message = "Operation completed successfully.")
        => new(true, data, message, []);

    /// <summary>Creates a failed typed result from a single error.</summary>
    public static new Result<T> Failure(Error error)
        => new(false, default, error.Description, [error]);

    /// <summary>Creates a failed typed result from multiple errors.</summary>
    public static new Result<T> Failure(IReadOnlyList<Error> errors, string message = "One or more errors occurred.")
        => new(false, default, message, errors);

    /// <summary>Creates a failed typed result from a plain message.</summary>
    public static new Result<T> Failure(string message)
        => new(false, default, message, [new Error("General.Error", message)]);
}
