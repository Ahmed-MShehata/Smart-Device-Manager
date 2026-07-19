using SDM.Application.Common.CQRS;

namespace SDM.Application.Company.UpdateCompanyProfile;

/// <summary>Command to update the active company profile.</summary>
public sealed class UpdateCompanyProfileCommand : ICommand
{
    public string CompanyName { get; init; } = string.Empty;
    public string? LogoUrl { get; init; }
    public string? Phone { get; init; }
    public string? WhatsApp { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public string? Facebook { get; init; }
    public string? Address { get; init; }
}
