using Moq;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.Users;

public class UsersReadOnlyRepositoryBuilder
{
    private readonly Mock<IUsersReadOnlyRepository> _repository;

    public UsersReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUsersReadOnlyRepository>();
    }

    public void ExistsUserWithSameEmailAsync(string email)
    {
        _repository.Setup(repository => repository.ExistsUserWithSameEmailAsync(email, CancellationToken.None))
                   .ReturnsAsync(true);
    }

    public UsersReadOnlyRepositoryBuilder GetByEmailAsync(User user)
    {
        _repository.Setup(repository => repository.GetByEmailAsync(user.Email, CancellationToken.None))
                   .ReturnsAsync(user);

        return this;
    }

    public UsersReadOnlyRepositoryBuilder GetByIdAsync(User? user)
    {
        if (user is not null)
        {
            _repository.Setup(repository => repository.GetByIdAsync(user.Id, CancellationToken.None))
                       .ReturnsAsync(user);
        }

        return this;
    }

    public IUsersReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}