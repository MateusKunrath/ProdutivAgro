using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Sales.Entities;

namespace ProdutivAgro.Infrastructure.Persistence.Configurations;

public sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales",
            table => { table.HasCheckConstraint("CK_Sales_TotalAmount_NonNegative", "\"TotalAmount\" >= 0"); });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.SoldAt).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.HasIndex(x => new { x.OrganizationId, x.SoldAt });
        builder.HasIndex(x => new { x.OrganizationId, x.Status });

        builder.HasOne<Organization>()
               .WithMany()
               .HasForeignKey(x => x.OrganizationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedByUser)
               .WithMany()
               .HasForeignKey(x => x.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(x => x.UpdatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(x => x.SaleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
