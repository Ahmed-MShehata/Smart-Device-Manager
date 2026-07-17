using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="DiagnosticChoice"/>.</summary>
internal sealed class DiagnosticChoiceConfiguration : IEntityTypeConfiguration<DiagnosticChoice>
{
    public void Configure(EntityTypeBuilder<DiagnosticChoice> builder)
    {
        builder.ToTable("DiagnosticChoices");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.QuestionId).IsRequired();

        builder.Property(x => x.ChoiceText)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ScoreValue)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt).IsRequired();

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();

        // Relationship configured in DiagnosticQuestionConfiguration
        builder.HasOne(c => c.Question)
            .WithMany(q => q.Choices)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
