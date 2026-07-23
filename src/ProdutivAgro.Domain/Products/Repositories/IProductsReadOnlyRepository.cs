using ProdutivAgro.Domain.Products.Entities;

namespace ProdutivAgro.Domain.Products.Repositories;

public interface IProductsReadOnlyRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
}