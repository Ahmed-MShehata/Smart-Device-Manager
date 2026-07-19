namespace SDM.Application.Company.GetCompanyProfile;

/// <summary>DTO representing the company profile shown in admin and customer views.</summary>
public sealed class CompanyProfileDto
{
    public Guid Id { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public string? LogoUrl { get; init; }
    public string? Phone { get; init; }
    public string? WhatsApp { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public string? Facebook { get; init; }
    public string? Address { get; init; }
    public bool IsActive { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
