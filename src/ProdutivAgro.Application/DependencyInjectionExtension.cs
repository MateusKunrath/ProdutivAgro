using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Application.AutoMapper;

namespace ProdutivAgro.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddMediatR(services);
        AddValidators(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddProfile<AutoMapping>());
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionExtension).Assembly);
        });
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtension).Assembly);
    }
}