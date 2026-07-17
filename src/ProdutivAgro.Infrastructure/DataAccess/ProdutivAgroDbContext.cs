using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Infrastructure.DataAccess;

public class ProdutivAgroDbContext : DbContext
{
    public ProdutivAgroDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>().HasIndex(o => o.Name).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
    }
}