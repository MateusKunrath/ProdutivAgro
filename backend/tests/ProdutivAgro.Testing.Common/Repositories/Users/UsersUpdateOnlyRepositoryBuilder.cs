using Moq;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.Users;

public class UsersUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUsersUpdateReadOnlyRepository> _repository;

    public UsersUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsersUpdateReadOnlyRepository>();
    }

    public UsersUpdateOnlyRepositoryBuilder GetById(User? user)
    {
        if (user is not null)
        {
            _repository.Setup(repository => repository.GetByIdAsync(user.Id, CancellationToken.None))
                       .ReturnsAsync(user);
        }

        return this;
    }

    public IUsersUpdateReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}