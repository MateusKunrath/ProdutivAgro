using Moq;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Services.AuthenticatedUser;

namespace CommonTestUtilities.AuthenticatedUser;

public class AuthenticatedUserBuilder
{
    public static IAuthenticatedUser Build(User user)
    {
        var mock = new Mock<IAuthenticatedUser>();
        mock.Setup(authenticatedUser => authenticatedUser.Get()).ReturnsAsync(user);
        return mock.Object;
    }
}