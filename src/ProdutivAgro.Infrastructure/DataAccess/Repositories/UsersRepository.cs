using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Repositories.Users;

namespace ProdutivAgro.Infrastructure.DataAccess.Repositories;

internal class UsersRepository(ProdutivAgroDbContext dbContext) : IUsersReadOnlyRepository, IUsersWriteOnlyRepository
{
    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await dbContext.Users.AsNoTracking().AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetUserByEmailOrPhoneNumber(string identifier)
    {
        return await dbContext
                     .Users
                     .AsNoTracking()
                     .FirstOrDefaultAsync(user => user.Email.Equals(identifier) || user.PhoneNumber.Equals(identifier));
    }

    async Task<User?> IUsersReadOnlyRepository.GetById(Guid id)
    {
        return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id.Equals(id));
    }

    public async Task<bool> ExistsActiveUserWithPhoneNumber(string phoneNumber)
    {
        return await dbContext.Users.AsNoTracking().AnyAsync(user => user.PhoneNumber.Equals(phoneNumber));
    }

    public async Task Add(User user)
    {
        await dbContext.Users.AddAsync(user);
    }
}