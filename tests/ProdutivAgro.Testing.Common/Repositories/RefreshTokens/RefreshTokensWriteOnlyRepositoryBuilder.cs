using Moq;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.RefreshTokens;

public class RefreshTokensWriteOnlyRepositoryBuilder
{
    public static IRefreshTokensWriteOnlyRepository Build()
    {
        var mock = new Mock<IRefreshTokensWriteOnlyRepository>();
        return mock.Object;
    }
}