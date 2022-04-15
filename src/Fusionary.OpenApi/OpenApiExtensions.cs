using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.OpenApi; 

public static class OpenApiExtensions {

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        return services
            .AddApiVersioning()
            .AddSwaggerGen()
            .AddVersionedApiExplorer();
    }
}
