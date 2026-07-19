using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDM.Domain.Entities;

namespace SDM.Infrastructure.Persistence.Configurations;

/// <summary>EF Core Fluent API configuration for <see cref="Order"/>.</summary>
internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CustomerName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CustomerPhone)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.CustomerWhatsApp)
            .HasMaxLength(30);

        builder.Property(x => x.CustomerGovernorate)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CustomerAddress)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(Domain.Enums.OrderStatus.Pending);

        // Audit fields
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UpdatedBy).HasMaxLength(100);

        // TotalPrice is a computed domain property — never stored
        builder.Ignore(x => x.TotalPrice);

        // Optimistic concurrency token — managed by SQL Server
        builder.Property(x => x.RowVersion).IsRowVersion();

        // ─── Items Collection (backing field) ──────────────────────────────
        builder.HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.Items)
            .HasField("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
