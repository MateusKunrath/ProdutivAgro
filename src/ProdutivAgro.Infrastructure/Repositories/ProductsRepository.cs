using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Products.Repositories;

namespace ProdutivAgro.Infrastructure.Repositories;

public class ProductsRepository : IProductsReadOnlyRepository
{
    public Task<Product?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}