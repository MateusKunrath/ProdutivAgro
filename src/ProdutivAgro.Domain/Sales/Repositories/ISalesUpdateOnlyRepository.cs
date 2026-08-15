using ProdutivAgro.Domain.Sales.Entities;

namespace ProdutivAgro.Domain.Sales.Repositories;

public interface ISalesUpdateOnlyRepository
{
    Task<Sale?> GetByIdAsync(Guid id, Guid organizationId, CancellationToken cancellationToken);
    void Update(Sale sale);
}