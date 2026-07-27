using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Repositories;

public class ProductsRepository(ProdutivAgroDbContext dbContext)
    : IProductsReadOnlyRepository, IProductsWriteOnlyRepository, IProductsUpdateOnlyRepository
{
    async Task<Product?> IProductsReadOnlyRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(Organization organization, CancellationToken cancellationToken)
    {
        return await dbContext.Products
                              .AsNoTracking()
                              .Where(x => x.OrganizationId == organization.Id)
                              .ToListAsync(cancellationToken);
    }

    public async Task<(List<Product> Items, int TotalCount)> GetPagedAsync(Guid organizationId, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Products
                             .AsNoTracking()
                             .Where(product => product.OrganizationId == organizationId)
                             .OrderBy(product => product.Description)
                             .ThenBy(product => product.Id);

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
                          .Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync(cancellationToken);

        return (items, totalItems);
    }

    public void Update(Product product)
    {
        dbContext.Products.Update(product);
    }

    async Task<Product?> IProductsUpdateOnlyRepository.GetByIdAsync(
        Guid id,
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Products.FirstOrDefaultAsync(
            p => p.Id == id && p.OrganizationId == organizationId,
            cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await dbContext.Products.AddAsync(product, cancellationToken);
    }
}
