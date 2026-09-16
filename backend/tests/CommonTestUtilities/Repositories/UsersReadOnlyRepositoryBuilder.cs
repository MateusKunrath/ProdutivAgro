using Moq;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories;

public class UsersReadOnlyRepositoryBuilder
{
    private readonly Mock<IUsersReadOnlyRepository> _repository;

    public UsersReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsersReadOnlyRepository>();
    }

    public void ExistsActiveUserWithEmail(string email)
    {
        _repository.Setup(usersReadOnly => usersReadOnly.ExistsActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public void ExistsActiveUserWithPhoneNumber(string phoneNumber)
    {
        _repository
            .Setup(usersReadOnly => usersReadOnly.ExistsActiveUserWithPhoneNumber(phoneNumber))
            .ReturnsAsync(true);
    }

    public UsersReadOnlyRepositoryBuilder GetUserByEmailOrPhoneNumber(User user)
    {
        _repository.Setup(usersReadOnly => usersReadOnly.GetUserByEmailOrPhoneNumber(user.Email)).ReturnsAsync(user);
        return this;
    }

    public UsersReadOnlyRepositoryBuilder GetUserById(User? user)
    {
        if (user is not null)
        {
            _repository.Setup(repository => repository.GetById(user.Id)).ReturnsAsync(user);
        }

        return this;
    }

    public IUsersReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}