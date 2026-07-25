using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;

public class UsersRepository(ProdutivAgroDbContext dbContext) : IUsersReadOnlyRepository, IUsersWriteOnlyRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> ExistsUserWithSameEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AsNoTracking().AnyAsync(x => x.Email.Equals(email), cancellationToken);
    }

    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }
}
