using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Application.Common.Security;
using ProdutivAgro.Domain.Identity.Services;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Domain.Repositories;
using ProdutivAgro.Domain.Security.Tokens;
using ProdutivAgro.Infrastructure.Identity;
using ProdutivAgro.Infrastructure.Identity.Jwt;
using ProdutivAgro.Infrastructure.Identity.Password;
using ProdutivAgro.Infrastructure.Persistence;
using ProdutivAgro.Infrastructure.Repositories;

namespace ProdutivAgro.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncrypter, BCryptPasswordHasher>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        AddToken(services, configuration);
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(_ => new JwtTokenGenerator(expirationTimeInMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        AddProductsRepository(services);
        // AddOrganizationsRepository(services);
        // AddUsersRepository(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ProdutivAgroDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ProdutivAgro.Infrastructure")));
    }

    // private static void AddOrganizationsRepository(IServiceCollection services)
    // {
    //     services.AddScoped<IOrganizationsReadOnlyRepository, OrganizationsRepository>();
    //     services.AddScoped<IOrganizationsWriteOnlyRepository, OrganizationsRepository>();
    //     services.AddScoped<IOrganizationsUpdateOnlyRepository, OrganizationsRepository>();
    // }
    //
    // private static void AddUsersRepository(IServiceCollection services)
    // {
    //     services.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
    //     services.AddScoped<IUsersWriteOnlyRepository, UsersRepository>();
    // }

    private static void AddProductsRepository(IServiceCollection services)
    {
        services.AddScoped<IProductsReadOnlyRepository, ProductsRepository>();
    }
}