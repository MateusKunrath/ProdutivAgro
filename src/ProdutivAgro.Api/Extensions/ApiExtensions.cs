using ProdutivAgro.Api.Filters;

namespace ProdutivAgro.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiExtensions(this IServiceCollection services)
    {
        services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
    }
}