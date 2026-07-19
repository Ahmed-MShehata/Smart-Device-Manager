using Microsoft.EntityFrameworkCore;
using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.Company.UpdateCompanyProfile;

/// <summary>Handles <see cref="UpdateCompanyProfileCommand"/>.</summary>
public sealed class UpdateCompanyProfileHandler : ICommandHandler<UpdateCompanyProfileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDb;

    public UpdateCompanyProfileHandler(IUnitOfWork unitOfWork, IReadDbContext readDb)
    {
        _unitOfWork = unitOfWork;
        _readDb = readDb;
    }

    public async Task<Result> Handle(
        UpdateCompanyProfileCommand command,
        CancellationToken cancellationToken)
    {
        var profile = await _readDb.CompanyProfiles
            .Where(c => c.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        if (profile is null)
        {
            // Create the profile for the first time
            var newProfile = new Domain.Entities.CompanyProfile(
                command.CompanyName,
                command.LogoUrl,
                command.Phone,
                command.WhatsApp,
                command.Email,
                command.Website,
                command.Facebook,
                command.Address);

            await _unitOfWork.CompanyProfiles.AddAsync(newProfile, cancellationToken);
        }
        else
        {
            profile.Update(
                command.CompanyName,
                command.LogoUrl,
                command.Phone,
                command.WhatsApp,
                command.Email,
                command.Website,
                command.Facebook,
                command.Address);

            _unitOfWork.CompanyProfiles.Update(profile);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Company profile updated successfully.");
    }
}
