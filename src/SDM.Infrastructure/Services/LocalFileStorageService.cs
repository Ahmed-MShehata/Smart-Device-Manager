using Microsoft.AspNetCore.Hosting;
using SDM.Application.Interfaces;

namespace SDM.Infrastructure.Services;

/// <summary>
/// Local disk implementation of <see cref="IFileStorageService"/>.
/// Stores product images under <c>{WebRoot}/images/products/</c>.
/// Images are served by ASP.NET Core's static-files middleware.
/// </summary>
/// <remarks>
/// <para>
/// If <see cref="IWebHostEnvironment.WebRootPath"/> is <see langword="null"/> (e.g., in
/// non-web or test hosts), the service falls back to <c>{ContentRoot}/wwwroot</c>,
/// creating the directory on first use if required.
/// </para>
/// <para>
/// This class intentionally swallows deletion errors.  A missing file during a delete
/// operation is not an error condition — it indicates either a prior clean-up or a
/// never-created file, both of which are safe to ignore.
/// </para>
/// </remarks>
internal sealed class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    /// <summary>Relative subfolder under the web root where product images are stored.</summary>
    private const string ImageSubfolder = "images/products";

    /// <summary>
    /// Initializes a new instance of <see cref="LocalFileStorageService"/>.
    /// </summary>
    public LocalFileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    /// <inheritdoc/>
    public async Task<string> SaveImageAsync(
        Stream imageStream,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var storageDirectory = GetStorageDirectory();
        Directory.CreateDirectory(storageDirectory);

        // Map the validated MIME type to a known safe extension.
        // The client-supplied filename is deliberately ignored.
        var extension = ContentTypeToExtension(contentType);
        var uniqueName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(storageDirectory, uniqueName);

        await using var fileStream = new FileStream(
            fullPath, FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: 81920, useAsync: true);

        await imageStream.CopyToAsync(fileStream, cancellationToken);

        // Return the relative path (forward slashes) for storage in the database.
        return $"{ImageSubfolder}/{uniqueName}";
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(
        string relativePath,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var webRoot = GetWebRoot();
            // Normalise path separators for the host OS
            var fullPath = Path.Combine(webRoot, relativePath.Replace('/', Path.DirectorySeparatorChar));

            if (!File.Exists(fullPath))
                return Task.FromResult(false);

            File.Delete(fullPath);
            return Task.FromResult(true);
        }
        catch
        {
            // Silently suppress all deletion errors.
            // The caller (handler) has already committed the DB update, so the system
            // remains in a consistent state regardless of whether this file delete succeeds.
            return Task.FromResult(false);
        }
    }

    // ─── Helpers ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Mapping of validated MIME types to safe file extensions.
    /// Must be kept in sync with <c>UploadProductImageValidator</c>'s allowed content types.
    /// </summary>
    private static readonly Dictionary<string, string> ExtensionMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["image/jpeg"] = ".jpg",
        ["image/png"]  = ".png",
        ["image/webp"] = ".webp"
    };

    /// <summary>Returns the file extension for a validated MIME type. Defaults to <c>.jpg</c>.</summary>
    private static string ContentTypeToExtension(string contentType)
        => ExtensionMap.TryGetValue(contentType, out var ext) ? ext : ".jpg";

    private string GetWebRoot()
        => _env.WebRootPath
           ?? Path.Combine(_env.ContentRootPath, "wwwroot");

    private string GetStorageDirectory()
        => Path.Combine(GetWebRoot(), "images", "products");

    /// <inheritdoc/>
    public string? GetAbsolutePath(string relativePath)
    {
        var webRoot  = GetWebRoot();
        var fullPath = Path.Combine(webRoot, relativePath.Replace('/', Path.DirectorySeparatorChar));
        return File.Exists(fullPath) ? fullPath : null;
    }
}
