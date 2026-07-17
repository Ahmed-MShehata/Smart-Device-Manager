using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="DiagnosticCategory"/>.</summary>
internal sealed class DiagnosticCategoryConfiguration : IEntityTypeConfiguration<DiagnosticCategory>
{
    public void Configure(EntityTypeBuilder<DiagnosticCategory> builder)
    {
        builder.ToTable("DiagnosticCategories");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.IconName)
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt).IsRequired();

        // Unique constraint on Name
        builder.HasIndex(x => x.Name).IsUnique();

        // ─── Questions collection (backing field) ──────────────────────────
        builder.HasMany(c => c.Questions)
            .WithOne(q => q.Category)
            .HasForeignKey(q => q.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Questions)
            .HasField("_questions")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // ─── Rules collection (backing field) ──────────────────────────────
        builder.HasMany(c => c.Rules)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Rules)
            .HasField("_rules")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
