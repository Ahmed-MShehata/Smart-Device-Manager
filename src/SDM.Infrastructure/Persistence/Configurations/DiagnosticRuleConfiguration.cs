using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="DiagnosticRule"/>.</summary>
internal sealed class DiagnosticRuleConfiguration : IEntityTypeConfiguration<DiagnosticRule>
{
    public void Configure(EntityTypeBuilder<DiagnosticRule> builder)
    {
        builder.ToTable("DiagnosticRules");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CategoryId).IsRequired();

        builder.Property(x => x.MinScore).IsRequired();
        builder.Property(x => x.MaxScore).IsRequired();

        builder.Property(x => x.Result)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Solution)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt).IsRequired();

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();

        // Relationship configured in DiagnosticCategoryConfiguration
        builder.HasOne(r => r.Category)
            .WithMany(c => c.Rules)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
