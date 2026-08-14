using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Products.Entities;

namespace ProdutivAgro.Domain.Products.Repositories;

public interface IProductsReadOnlyRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids, Guid organizationId, CancellationToken cancellationToken);
    Task<List<Product>> GetAllAsync(Organization organization, CancellationToken cancellationToken);

    Task<(List<Product> Items, int TotalCount)> GetPagedAsync(
        Guid organizationId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}