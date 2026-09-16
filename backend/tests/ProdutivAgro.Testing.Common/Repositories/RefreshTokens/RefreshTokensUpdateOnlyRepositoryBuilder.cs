using Moq;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.RefreshTokens;

public class RefreshTokensUpdateOnlyRepositoryBuilder
{
    public static IRefreshTokensUpdateOnlyRepository Build()
    {
        var mock = new Mock<IRefreshTokensUpdateOnlyRepository>();
        return mock.Object;
    }
}