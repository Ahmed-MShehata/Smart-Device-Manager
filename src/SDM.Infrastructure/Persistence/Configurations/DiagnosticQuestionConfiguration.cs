using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="DiagnosticQuestion"/>.</summary>
internal sealed class DiagnosticQuestionConfiguration : IEntityTypeConfiguration<DiagnosticQuestion>
{
    public void Configure(EntityTypeBuilder<DiagnosticQuestion> builder)
    {
        builder.ToTable("DiagnosticQuestions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CategoryId).IsRequired();

        builder.Property(x => x.QuestionText)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.OrderIndex)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt).IsRequired();

        // ─── Choices collection (backing field) ────────────────────────────
        builder.HasMany(q => q.Choices)
            .WithOne(c => c.Question)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(q => q.Choices)
            .HasField("_choices")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
