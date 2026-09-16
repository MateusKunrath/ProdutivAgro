using Moq;
using ProdutivAgro.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories;

public class UsersWriteOnlyRepositoryBuilder
{
    public static IUsersWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsersWriteOnlyRepository>();
        return mock.Object;
    }
}