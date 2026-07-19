using SDM.Application.Common.CQRS;

namespace SDM.Application.Company.GetCompanyProfile;

/// <summary>Query to retrieve the active company profile.</summary>
public sealed class GetCompanyProfileQuery : IQuery<CompanyProfileDto> { }
