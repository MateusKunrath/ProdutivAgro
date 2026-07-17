using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Infrastructure.DataAccess;

namespace ProdutivAgro.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ProdutivAgroDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}