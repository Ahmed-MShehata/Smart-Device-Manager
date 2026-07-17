using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="SystemComponent"/>.</summary>
internal sealed class SystemComponentConfiguration : IEntityTypeConfiguration<SystemComponent>
{
    public void Configure(EntityTypeBuilder<SystemComponent> builder)
    {
        builder.ToTable("SystemComponents");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Version)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.SilentInstallCommand)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.SHA256)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Size)
            .IsRequired();

        builder.Property(x => x.RequiresRestart)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(Domain.Enums.ComponentStatus.Active);

        // Audit fields
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy).HasMaxLength(100);
    }
}
