using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Repositories.Users;

public interface IUsersReadOnlyRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<bool> ExistsActiveUserWithPhoneNumber(string phoneNumber);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByPhoneNumber(string phoneNumber);
    Task<User?> GetById(Guid id);
}