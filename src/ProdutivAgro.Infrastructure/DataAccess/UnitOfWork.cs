using ProdutivAgro.Domain.Repositories;

namespace ProdutivAgro.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProdutivAgroDbContext _dbContext;

    public UnitOfWork(ProdutivAgroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}