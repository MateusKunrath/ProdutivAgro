using ProdutivAgro.Domain.Sales.Entities;

namespace ProdutivAgro.Domain.Sales.Repositories;

public interface ISalesWriteOnlyRepository
{
    Task AddAsync(Sale sale, CancellationToken cancellationToken);

    Task AddItemsAsync(IEnumerable<SaleItem> items, CancellationToken cancellationToken);
}
