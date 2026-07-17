using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Application.AutoMapper;
using ProdutivAgro.Application.UseCases.Authentication.Authenticate;
using ProdutivAgro.Application.UseCases.Organizations.Create;
using ProdutivAgro.Application.UseCases.Users.Create;

namespace ProdutivAgro.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddProfile<AutoMapping>());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        AddOrganizationUseCases(services);
        AddUserUseCases(services);
        AddAuthenticationUseCases(services);
    }

    private static void AddOrganizationUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateOrganizationUseCase, CreateOrganizationUseCase>();
    }

    private static void AddUserUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
    }

    private static void AddAuthenticationUseCases(IServiceCollection services)
    {
        services.AddScoped<IAuthenticateUseCase, AuthenticateUseCase>();
    }
}