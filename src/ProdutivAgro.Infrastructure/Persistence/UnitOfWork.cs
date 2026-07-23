using ProdutivAgro.Application.Abstractions.Persistence;

namespace ProdutivAgro.Infrastructure.Persistence;

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