using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="Setting"/>.</summary>
internal sealed class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Value)
            .HasMaxLength(2000)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.IsPublic)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt).IsRequired();

        // Unique constraint on Key
        builder.HasIndex(x => x.Key).IsUnique();

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();

        // ─── Seed Data ─────────────────────────────────────────────────────
        builder.HasData(
            new
            {
                Id = new Guid("22222222-2222-2222-2222-222222222201"),
                Key = "company.name",
                Value = "Smart Device Manager",
                Description = "The company or product name displayed in the application.",
                IsPublic = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = new Guid("22222222-2222-2222-2222-222222222202"),
                Key = "company.whatsapp",
                Value = "",
                Description = "WhatsApp contact number for customer support.",
                IsPublic = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = new Guid("22222222-2222-2222-2222-222222222203"),
                Key = "company.support_email",
                Value = "",
                Description = "Support email address shown to customers.",
                IsPublic = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = new Guid("22222222-2222-2222-2222-222222222204"),
                Key = "company.support_phone",
                Value = "",
                Description = "Support phone number shown to customers.",
                IsPublic = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = new Guid("22222222-2222-2222-2222-222222222205"),
                Key = "app.version",
                Value = "1.0.0",
                Description = "Current application version.",
                IsPublic = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
