using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Infrastructure.Identity;
using ProdutivAgro.Infrastructure.Identity.Jwt;
using ProdutivAgro.Infrastructure.Identity.Password;
using ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;
using ProdutivAgro.Infrastructure.Identity.RefreshTokens;
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
        var refreshTokenExpirationDays = configuration.GetValue<uint>("Settings:RefreshToken:ExpiresDays", 30);

        services.AddScoped<IJwtTokenGenerator>(_ => new JwtTokenGenerator(expirationTimeInMinutes, signingKey!));
        services.AddSingleton<IRefreshTokenService>(_ => new RefreshTokenService(refreshTokenExpirationDays));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        AddProductsRepository(services);
        AddOrganizationsRepository(services);
        AddUsersRepository(services);
        AddRefreshTokensRepository(services);
        AddSalesRepository(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ProdutivAgroDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ProdutivAgro.Infrastructure")));
    }

    private static void AddOrganizationsRepository(IServiceCollection services)
    {
        services.AddScoped<IOrganizationsReadOnlyRepository, OrganizationsRepository>();
        services.AddScoped<IOrganizationsWriteOnlyRepository, OrganizationsRepository>();
        services.AddScoped<IOrganizationsUpdateReadOnlyRepository, OrganizationsRepository>();
    }

    private static void AddUsersRepository(IServiceCollection services)
    {
        services.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
        services.AddScoped<IUsersWriteOnlyRepository, UsersRepository>();
        services.AddScoped<IUsersUpdateReadOnlyRepository, UsersRepository>();
    }

    private static void AddProductsRepository(IServiceCollection services)
    {
        services.AddScoped<IProductsReadOnlyRepository, ProductsRepository>();
        services.AddScoped<IProductsWriteOnlyRepository, ProductsRepository>();
        services.AddScoped<IProductsUpdateOnlyRepository, ProductsRepository>();
    }

    private static void AddRefreshTokensRepository(IServiceCollection services)
    {
        services.AddScoped<IRefreshTokensReadOnlyRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokensWriteOnlyRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokensUpdateOnlyRepository, RefreshTokenRepository>();
    }

    private static void AddSalesRepository(IServiceCollection services)
    {
        services.AddScoped<ISalesReadOnlyRepository, SalesRepository>();
        services.AddScoped<ISalesWriteOnlyRepository, SalesRepository>();
    }
}