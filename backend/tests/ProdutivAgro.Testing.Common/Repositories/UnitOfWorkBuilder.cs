using Moq;
using ProdutivAgro.Application.Abstractions.Persistence;

namespace ProdutivAgro.Testing.Common.Repositories;

public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();
        return mock.Object;
    }
}