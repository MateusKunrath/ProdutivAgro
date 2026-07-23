namespace ProdutivAgro.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task Commit();
}