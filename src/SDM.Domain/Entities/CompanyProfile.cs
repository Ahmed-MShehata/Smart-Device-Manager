using SDM.Domain.Common;

namespace SDM.Domain.Entities;

/// <summary>
/// Represents the company profile configured by the admin and displayed
/// to customers in the Company Information page.
///
/// Only one active profile exists at a time (IsActive = true).
/// The IsActive flag supports future multi-company scenarios without schema changes.
///
/// Audit fields are stamped automatically by Infrastructure on save.
/// </summary>
public class CompanyProfile : AuditableEntity
{
    /// <summary>Gets the display name of the company.</summary>
    public string CompanyName { get; private set; } = string.Empty;

    /// <summary>Gets the server-side relative path to the company logo. Null if not uploaded.</summary>
    public string? LogoUrl { get; private set; }

    /// <summary>Gets the company support phone number.</summary>
    public string? Phone { get; private set; }

    /// <summary>Gets the company WhatsApp number.</summary>
    public string? WhatsApp { get; private set; }

    /// <summary>Gets the company contact email address.</summary>
    public string? Email { get; private set; }

    /// <summary>Gets the company website URL.</summary>
    public string? Website { get; private set; }

    /// <summary>Gets the company Facebook page URL.</summary>
    public string? Facebook { get; private set; }

    /// <summary>Gets the physical address of the company.</summary>
    public string? Address { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this is the active company profile.
    /// Defaults to true. Supports future multi-company scenarios.
    /// </summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>Required by EF Core. Do not use directly.</summary>
    protected CompanyProfile() { }

    /// <summary>
    /// Creates a new <see cref="CompanyProfile"/>.
    /// IsActive defaults to true.
    /// Audit fields are stamped by Infrastructure on save.
    /// </summary>
    public CompanyProfile(
        string companyName,
        string? logoUrl = null,
        string? phone = null,
        string? whatsApp = null,
        string? email = null,
        string? website = null,
        string? facebook = null,
        string? address = null)
    {
        CompanyName = companyName;
        LogoUrl = logoUrl;
        Phone = phone;
        WhatsApp = whatsApp;
        Email = email;
        Website = website;
        Facebook = facebook;
        Address = address;
        IsActive = true;
    }

    /// <summary>Updates all editable fields of the company profile.</summary>
    public void Update(
        string companyName,
        string? logoUrl,
        string? phone,
        string? whatsApp,
        string? email,
        string? website,
        string? facebook,
        string? address)
    {
        CompanyName = companyName;
        LogoUrl = logoUrl;
        Phone = phone;
        WhatsApp = whatsApp;
        Email = email;
        Website = website;
        Facebook = facebook;
        Address = address;
    }
}
