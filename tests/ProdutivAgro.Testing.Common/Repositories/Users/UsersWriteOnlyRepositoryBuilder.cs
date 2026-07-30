using Moq;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.Users;

public class UsersWriteOnlyRepositoryBuilder
{
    public static IUsersWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsersWriteOnlyRepository>();
        return mock.Object;
    }
}