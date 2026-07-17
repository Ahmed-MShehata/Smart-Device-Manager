namespace SDM.Application.Interfaces;

/// <summary>
/// Provides access to the identity of the currently authenticated user (administrator).
/// Abstracts the HTTP context from the Application layer.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the username of the currently authenticated admin.
    /// Returns null for anonymous (customer) requests.
    /// </summary>
    string? Username { get; }

    /// <summary>
    /// Gets the unique identifier of the currently authenticated admin.
    /// Returns null for anonymous (customer) requests.
    /// </summary>
    string? UserId { get; }

    /// <summary>Gets the role of the currently authenticated admin. Null for anonymous requests.</summary>
    string? Role { get; }

    /// <summary>Gets a value indicating whether the current request is from an authenticated admin.</summary>
    bool IsAuthenticated { get; }
}
