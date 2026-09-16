namespace ProdutivAgro.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task Commit();

    Task ExecuteInTransactionAsync(
        Func<CancellationToken, Task> action,
        CancellationToken cancellationToken
    );
}