using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents a software application or driver that the admin uploads
/// for customers to browse and install via the Software Center.
///
/// Update workflow:
///   - First time: Admin creates with all fields including setup file.
///   - Future versions: Admin calls <see cref="UpdateSetupFile"/> providing
///     a new setup file. Name, Category, Description, and IconUrl are preserved.
///
/// Version is extracted automatically from the file when possible.
/// No silent install command. No detection rule. No installer type.
/// Customers run the standard setup wizard.
///
/// Audit fields are stamped automatically by Infrastructure on save.
/// CreatedAt = upload date. UpdatedAt = last file replacement date.
/// </summary>
public class SoftwarePackage : AuditableEntity
{
    /// <summary>Gets the display name of the software.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Gets the category: Application or Driver.</summary>
    public string Category { get; private set; } = string.Empty;

    /// <summary>Gets the version string (e.g., "1.0.0"). Auto-extracted when possible.</summary>
    public string Version { get; private set; } = string.Empty;

    /// <summary>Gets the admin-authored description of what this software does.</summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>Gets the server-side relative path to the icon image. Null if not uploaded.</summary>
    public string? IconUrl { get; private set; }

    /// <summary>Gets the server-side relative path to the setup file.</summary>
    public string SetupFileUrl { get; private set; } = string.Empty;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected SoftwarePackage() { }

    /// <summary>
    /// Creates a new <see cref="SoftwarePackage"/>.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    /// <param name="name">Display name. Required.</param>
    /// <param name="category">Application or Driver. Required.</param>
    /// <param name="version">Version string. Required.</param>
    /// <param name="description">Description. Required.</param>
    /// <param name="setupFileUrl">Setup file path. Required.</param>
    /// <param name="iconUrl">Optional icon path.</param>
    public SoftwarePackage(
        string name,
        string category,
        string version,
        string description,
        string setupFileUrl,
        string? iconUrl = null)
    {
        Name = name;
        Category = category;
        Version = version;
        Description = description;
        SetupFileUrl = setupFileUrl;
        IconUrl = iconUrl;
    }

    /// <summary>
    /// Updates the metadata fields: Name, Category, Description, IconUrl.
    /// Does NOT touch the setup file or version.
    /// </summary>
    public void UpdateMetadata(
        string name,
        string category,
        string description,
        string? iconUrl)
    {
        Name = name;
        Category = category;
        Description = description;
        IconUrl = iconUrl;
    }

    /// <summary>
    /// Replaces the setup file and updates the version.
    /// Name, Category, Description, and IconUrl are preserved.
    /// The audit UpdatedAt is stamped automatically by Infrastructure.
    /// </summary>
    /// <param name="newSetupFileUrl">New setup file path. Required.</param>
    /// <param name="newVersion">New version string (auto-extracted or manual).</param>
    public void UpdateSetupFile(string newSetupFileUrl, string newVersion)
    {
        SetupFileUrl = newSetupFileUrl;
        Version = newVersion;
    }
}
