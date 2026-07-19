using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="KnowledgeBaseArticle"/>.</summary>
internal sealed class KnowledgeBaseArticleConfiguration : IEntityTypeConfiguration<KnowledgeBaseArticle>
{
    public void Configure(EntityTypeBuilder<KnowledgeBaseArticle> builder)
    {
        builder.ToTable("KnowledgeBaseArticles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ProblemName)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(10000);

        builder.Property(x => x.ProblemImageUrl)
            .HasMaxLength(500);

        builder.Property(x => x.YouTubeVideoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DisplayOrder)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.Visible)
            .IsRequired()
            .HasDefaultValue(true);

        // Audit fields
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy).HasMaxLength(100);

        // Index on DisplayOrder for efficient sort queries
        builder.HasIndex(x => x.DisplayOrder);

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}
