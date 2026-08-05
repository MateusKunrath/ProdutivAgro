using Moq;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Testing.Common.CurrentUser;

public class CurrentUserBuilder
{
    private readonly Mock<ICurrentUser> _currentUser;

    public CurrentUserBuilder()
    {
        _currentUser = new Mock<ICurrentUser>();
    }

    public void DefineUser(User user)
    {
        _currentUser.Setup(currentUser => currentUser.UserId).Returns(user.Id);
        _currentUser.Setup(currentUser => currentUser.IsAuthenticated).Returns(true);
        _currentUser.Setup(currentUser => currentUser.OrganizationId).Returns(user.OrganizationId);
        _currentUser.Setup(currentUser => currentUser.Role).Returns(user.Role);
    }

    public ICurrentUser Build()
    {
        return _currentUser.Object;
    }
}