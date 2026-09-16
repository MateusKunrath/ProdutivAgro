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

    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await action(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}