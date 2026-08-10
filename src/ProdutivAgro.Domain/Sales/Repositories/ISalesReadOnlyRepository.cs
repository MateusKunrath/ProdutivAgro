using ProdutivAgro.Domain.Sales.Entities;

namespace ProdutivAgro.Domain.Sales.Repositories;

public interface ISalesReadOnlyRepository
{
    Task<Sale?> GetByIdAsync(Guid id, Guid organizationId, CancellationToken cancellationToken);

    Task<(List<Sale> Items, int TotalCount)> GetPagedAsync(
        Guid organizationId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}