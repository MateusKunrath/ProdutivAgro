namespace ProdutivAgro.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}