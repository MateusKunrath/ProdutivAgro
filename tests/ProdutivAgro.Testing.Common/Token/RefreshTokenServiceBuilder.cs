using Moq;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Testing.Common.Token;

public class RefreshTokenServiceBuilder
{
    public static IRefreshTokenService Build()
    {
        var mock = new Mock<IRefreshTokenService>();

        mock.Setup(refreshTokenService => refreshTokenService.Generate())
            .Returns("refresh-token");

        mock.Setup(refreshTokenService => refreshTokenService.Hash(It.IsAny<string>()))
            .Returns("hashed-refresh-token");

        mock.Setup(refreshTokenService => refreshTokenService.GetExpirationDate(It.IsAny<DateTimeOffset>()))
            .Returns((DateTimeOffset now) => now.AddDays(7));

        return mock.Object;
    }
}
