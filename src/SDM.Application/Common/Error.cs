namespace SDM.Application.Common;

/// <summary>
/// Represents a discrete error with a code and human-readable description.
/// Used within <see cref="Result"/> and <see cref="Result{T}"/> to communicate failure reasons.
/// </summary>
public sealed record Error
{
    /// <summary>Gets the machine-readable error code (e.g., "Product.NotFound").</summary>
    public string Code { get; }

    /// <summary>Gets the human-readable error description.</summary>
    public string Description { get; }

    /// <summary>
    /// Initializes a new <see cref="Error"/>.
    /// </summary>
    /// <param name="code">Machine-readable error code.</param>
    /// <param name="description">Human-readable error description.</param>
    public Error(string code, string description)
    {
        Code = code;
        Description = description;
    }

    // ─── Well-Known Errors ────────────────────────────────────────────────

    /// <summary>Represents the absence of an error. Used for successful results.</summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>Returns a "not found" error for the given resource name.</summary>
    public static Error NotFound(string resource) =>
        new($"{resource}.NotFound", $"{resource} was not found.");

    /// <summary>Returns a "conflict" error when a resource already exists.</summary>
    public static Error Conflict(string resource) =>
        new($"{resource}.Conflict", $"{resource} already exists.");

    /// <summary>Returns a "validation" error for invalid input.</summary>
    public static Error Validation(string field, string message) =>
        new($"Validation.{field}", message);

    /// <summary>Returns a generic "unauthorized" error.</summary>
    public static Error Unauthorized() =>
        new("Auth.Unauthorized", "You are not authorized to perform this action.");

    /// <summary>Returns a generic "forbidden" error.</summary>
    public static Error Forbidden() =>
        new("Auth.Forbidden", "Access to this resource is forbidden.");

    /// <inheritdoc/>
    public override string ToString() => $"{Code}: {Description}";
}
