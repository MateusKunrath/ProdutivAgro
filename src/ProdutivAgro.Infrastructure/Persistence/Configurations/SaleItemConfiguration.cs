using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutivAgro.Domain.Sales.Entities;

namespace ProdutivAgro.Infrastructure.Persistence.Configurations;

public sealed class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems", table =>
        {
            table.HasCheckConstraint("CK_SaleItems_Quantity_Positive", "\"Quantity\" > 0");
            table.HasCheckConstraint("CK_SaleItems_UnitPrice_NonNegative", "\"UnitPrice\" >= 0");
            table.HasCheckConstraint("CK_SaleItems_TotalAmount_NonNegative", "\"TotalAmount\" >= 0");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductDescription).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Unit).IsRequired();
        builder.Property(x => x.Quantity).HasPrecision(18, 3).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2).IsRequired();

        builder.HasIndex(x => x.SaleId);
        builder.HasIndex(x => x.SourceProductId);
    }
}
