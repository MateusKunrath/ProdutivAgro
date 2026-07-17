using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Domain.Repositories;
using ProdutivAgro.Domain.Repositories.Organizations;
using ProdutivAgro.Domain.Repositories.Users;
using ProdutivAgro.Domain.Security.Cryptography;
using ProdutivAgro.Domain.Security.Tokens;
using ProdutivAgro.Domain.Services.AuthenticatedUser;
using ProdutivAgro.Infrastructure.DataAccess;
using ProdutivAgro.Infrastructure.DataAccess.Repositories;
using ProdutivAgro.Infrastructure.Security.Tokens;
using ProdutivAgro.Infrastructure.Services.AuthenticatedUser;

namespace ProdutivAgro.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

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

        AddOrganizationsRepository(services);
        AddUsersRepository(services);
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
        services.AddScoped<IOrganizationsUpdateOnlyRepository, OrganizationsRepository>();
    }

    private static void AddUsersRepository(IServiceCollection services)
    {
        services.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
        services.AddScoped<IUsersWriteOnlyRepository, UsersRepository>();
    }
}