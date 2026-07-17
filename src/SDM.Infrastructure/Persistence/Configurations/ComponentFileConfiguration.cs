using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="ComponentFile"/>.</summary>
internal sealed class ComponentFileConfiguration : IEntityTypeConfiguration<ComponentFile>
{
    public void Configure(EntityTypeBuilder<ComponentFile> builder)
    {
        builder.ToTable("ComponentFiles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ComponentId).IsRequired();

        builder.Property(x => x.FileType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.StoredFileName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.OriginalFileName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.RelativePath)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.MimeType)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.SHA256)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Version)
            .HasMaxLength(50);

        builder.Property(x => x.UploadedAt).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();

        // ─── Relationship ──────────────────────────────────────────────────
        builder.HasOne(f => f.Component)
            .WithMany()
            .HasForeignKey(f => f.ComponentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
