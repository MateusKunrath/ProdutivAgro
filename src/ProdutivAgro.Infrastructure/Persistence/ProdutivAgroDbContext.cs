using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Products.Entities;

namespace ProdutivAgro.Infrastructure.Persistence;

public class ProdutivAgroDbContext : DbContext
{
    public ProdutivAgroDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ProdutivAgroDbContext).Assembly);
    }
}