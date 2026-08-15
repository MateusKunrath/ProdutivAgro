using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Repositories;

public class SalesRepository(ProdutivAgroDbContext dbContext)
    : ISalesReadOnlyRepository, ISalesWriteOnlyRepository, ISalesUpdateOnlyRepository
{
    async Task<Sale?> ISalesReadOnlyRepository.GetByIdAsync(Guid id, Guid organizationId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Sales
                              .AsNoTracking()
                              .Include(x => x.Items)
                              .Where(x => x.OrganizationId == organizationId)
                              .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(List<Sale> Items, int TotalCount)> GetPagedAsync(
        Guid organizationId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = dbContext.Sales
                             .AsNoTracking()
                             .Include(x => x.CreatedByUser)
                             .Where(x => x.OrganizationId == organizationId)
                             .OrderByDescending(x => x.SoldAt)
                             .ThenByDescending(x => x.Id);

        var totalCount = await query.CountAsync(cancellationToken);

        var sales = await query
                          .Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize)
                          .Include(x => x.Items)
                          .ToListAsync(cancellationToken);

        return (sales, totalCount);
    }

    async Task<Sale?> ISalesUpdateOnlyRepository.GetByIdAsync(Guid id, Guid organizationId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Sales
                              .Include(x => x.Items)
                              .Where(x => x.OrganizationId == organizationId)
                              .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Update(Sale sale)
    {
        dbContext.Sales.Update(sale);
    }

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
    {
        await dbContext.Sales.AddAsync(sale, cancellationToken);
    }
}