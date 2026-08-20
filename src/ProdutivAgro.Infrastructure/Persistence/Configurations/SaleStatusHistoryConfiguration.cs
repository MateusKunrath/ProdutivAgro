using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutivAgro.Domain.Sales.Entities;

namespace ProdutivAgro.Infrastructure.Persistence.Configurations;

public sealed class SaleStatusHistoryConfiguration : IEntityTypeConfiguration<SaleStatusHistory>
{
    public void Configure(EntityTypeBuilder<SaleStatusHistory> builder)
    {
        builder.ToTable("SalesStatusHistory");

        builder.HasKey(pk => pk.Id);

        builder.Property(x => x.ChangedAt).IsRequired();
        builder.Property(x => x.PreviousStatus).IsRequired();
        builder.Property(x => x.CurrentStatus).IsRequired();
        builder.Property(x => x.Reason).HasMaxLength(500);

        builder.HasIndex(x => new { x.PreviousStatus });
        builder.HasIndex(x => new { x.CurrentStatus });
        builder.HasIndex(x => new { x.SaleId });

        builder.HasOne(x => x.Sale)
               .WithMany(x => x.SalesStatusHistory)
               .HasForeignKey(x => x.SaleId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ChangedByUser)
               .WithMany()
               .HasForeignKey(x => x.ChangedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
