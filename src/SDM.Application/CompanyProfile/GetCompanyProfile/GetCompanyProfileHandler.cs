using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;

namespace SDM.Application.Company.GetCompanyProfile;

/// <summary>Handles <see cref="GetCompanyProfileQuery"/>.</summary>
public sealed class GetCompanyProfileHandler : IQueryHandler<GetCompanyProfileQuery, CompanyProfileDto>
{
    private readonly IReadDbContext _db;

    public GetCompanyProfileHandler(IReadDbContext db) => _db = db;

    public async Task<Result<CompanyProfileDto>> Handle(
        GetCompanyProfileQuery query,
        CancellationToken cancellationToken)
    {
        var profile = await _db.CompanyProfiles
            .Where(c => c.IsActive)
            .Select(c => new CompanyProfileDto
            {
                Id          = c.Id,
                CompanyName = c.CompanyName,
                LogoUrl     = c.LogoUrl,
                Phone       = c.Phone,
                WhatsApp    = c.WhatsApp,
                Email       = c.Email,
                Website     = c.Website,
                Facebook    = c.Facebook,
                Address     = c.Address,
                IsActive    = c.IsActive,
                UpdatedAt   = c.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (profile is null)
            return Result<CompanyProfileDto>.Failure(Error.NotFound("CompanyProfile"));

        return Result<CompanyProfileDto>.Success(profile);
    }
}
