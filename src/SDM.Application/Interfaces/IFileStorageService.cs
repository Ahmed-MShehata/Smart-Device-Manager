namespace SDM.Application.Interfaces;

/// <summary>
/// Abstraction over the physical file storage layer.
/// Application handlers depend on this interface — the implementation
/// lives in Infrastructure and is responsible for all I/O operations.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Persists an image stream to storage and returns the relative path
    /// that should be stored in the database.
    /// </summary>
    /// <param name="imageStream">The readable image content stream.</param>
    /// <param name="originalFileName">
    /// The original client file name. Used only to determine the file extension.
    /// A new unique name is always generated — the original name is never kept.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// A relative path (e.g., <c>images/products/abc123.jpg</c>) suitable for
    /// storing in the database and constructing a public URL.
    /// </returns>
    Task<string> SaveImageAsync(
        Stream imageStream,
        string contentType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a file from storage.
    /// Errors are suppressed — no exception is thrown if the file does not exist.
    /// </summary>
    /// <param name="relativePath">
    /// The relative path previously returned by <see cref="SaveImageAsync"/>.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><see langword="true"/> if the file was found and deleted; otherwise <see langword="false"/>.</returns>
    Task<bool> DeleteAsync(
        string relativePath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Resolves a relative path (as stored in the database) to an absolute filesystem path.
    /// Returns <see langword="null"/> if the file does not exist.
    /// This method must never be called from the Application layer directly —
    /// only the API/Infrastructure layer may call it to serve file downloads.
    /// </summary>
    string? GetAbsolutePath(string relativePath);
}
