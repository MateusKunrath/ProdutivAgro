namespace ProdutivAgro.Api.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }
}
