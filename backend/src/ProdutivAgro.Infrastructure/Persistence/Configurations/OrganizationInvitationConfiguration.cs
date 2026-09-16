using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Infrastructure.Persistence.Configurations;

public sealed class OrganizationInvitationConfiguration : IEntityTypeConfiguration<OrganizationInvitation>
{
    public void Configure(EntityTypeBuilder<OrganizationInvitation> builder)
    {
        builder.ToTable("OrganizationInvitations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.TokenHash).IsRequired().HasMaxLength(88);
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasIndex(x => x.TokenHash).IsUnique();
        builder.HasIndex(x => new { x.OrganizationId, x.CreatedAt });
        builder.HasIndex(x => new { x.OrganizationId, x.Email })
               .IsUnique()
               .HasFilter("\"AcceptedAt\" IS NULL AND \"RevokedAt\" IS NULL");

        builder.HasOne<Organization>()
               .WithMany()
               .HasForeignKey(x => x.OrganizationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.InvitedByUser)
               .WithMany()
               .HasForeignKey(x => x.InvitedByUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}