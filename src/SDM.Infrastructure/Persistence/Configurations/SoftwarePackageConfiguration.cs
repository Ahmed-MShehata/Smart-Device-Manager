using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="SoftwarePackage"/>.</summary>
internal sealed class SoftwarePackageConfiguration : IEntityTypeConfiguration<SoftwarePackage>
{
    public void Configure(EntityTypeBuilder<SoftwarePackage> builder)
    {
        builder.ToTable("SoftwarePackages");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Category is stored as a string (Application | Driver) for readability in the database.
        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Version)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.SetupFileUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.IconUrl)
            .HasMaxLength(500);

        // Audit fields — CreatedAt = upload date, UpdatedAt = last file replacement date
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy).HasMaxLength(100);

        // Name must be unique
        builder.HasIndex(x => x.Name).IsUnique();

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
