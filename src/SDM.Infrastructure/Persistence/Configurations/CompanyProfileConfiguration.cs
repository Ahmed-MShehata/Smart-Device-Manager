using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="CompanyProfile"/>.</summary>
internal sealed class CompanyProfileConfiguration : IEntityTypeConfiguration<CompanyProfile>
{
    public void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        builder.ToTable("CompanyProfiles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.LogoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Phone)
            .HasMaxLength(30);

        builder.Property(x => x.WhatsApp)
            .HasMaxLength(30);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.Website)
            .HasMaxLength(500);

        builder.Property(x => x.Facebook)
            .HasMaxLength(500);

        builder.Property(x => x.Address)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Audit fields
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy).HasMaxLength(100);

        // Only one active profile should exist at a time
        builder.HasIndex(x => x.IsActive);

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
