using ProdutivAgro.Domain.Products.Entities;

namespace ProdutivAgro.Domain.Products.Repositories;

public interface IProductsUpdateOnlyRepository
{
    Task<Product?> GetByIdAsync(Guid id, Guid organizationId, CancellationToken cancellationToken);
    void Update(Product product);
}
