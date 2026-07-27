using ProdutivAgro.Domain.Products.Entities;

namespace ProdutivAgro.Domain.Products.Repositories;

public interface IProductsWriteOnlyRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);
}