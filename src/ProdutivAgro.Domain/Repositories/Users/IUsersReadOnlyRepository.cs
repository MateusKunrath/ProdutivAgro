using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Repositories.Users;

public interface IUsersReadOnlyRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<bool> ExistsActiveUserWithPhoneNumber(string phoneNumber);
    Task<User?> GetUserByEmailOrPhoneNumber(string identifier);
    Task<User?> GetById(Guid id);
}