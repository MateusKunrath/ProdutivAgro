using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProdutivAgro.Application.Behaviors;

namespace ProdutivAgro.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddMediatR(services);
        AddValidators(services);
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionExtension).Assembly);
            config.AddOpenBehavior(typeof(ActiveOrganizationBehavior<,>));
        });
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtension).Assembly);
    }
}
