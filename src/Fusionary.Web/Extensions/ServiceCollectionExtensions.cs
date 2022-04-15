using System.Reflection;

using Fusionary.Web.ConfigureOptions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.Web.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddStandardWebServices(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(Lazy<>), typeof(LazyService<>))
            .AddMemoryCache()
            .AddCors()
            .AddResponseCaching()
            .AddResponseCompression(options => {
                options.EnableForHttps = true;
            })
            .AddHttpContextAccessor()
            .AddSingleton<IActionContextAccessor, ActionContextAccessor>();
    }

    public static IMvcBuilder AddMvcServices(this IServiceCollection services)
    {
        return services
            .AddRouting()
            .AddControllers();
    }

    public static WebApplication UseStandardWebService(this WebApplication app)
    {
        app
            .UseForwardedHeaders()
            .UseResponseCaching()
            .UseResponseCompression()
            .UseIf(
                app.Environment.IsLocalOrDevelopment(),
                x => x.UseDeveloperExceptionPage()
            )
            .UseStaticFiles()
            .UseHttpsRedirection();

        return app;
    }

    /// <summary>
    ///     Adds types that implement <typeparamref name="T" /> to the services collection
    /// </summary>
    /// <param name="services">The services registry</param>
    /// <param name="assemblies">The source assemblies</param>
    /// <param name="lifetime">The service lifetime (defaults to Singleton)</param>
    /// <exception cref="ArgumentOutOfRangeException">The <typeparamref name="T" /> must be an interface</exception>
    /// <typeparam name="T">The base interface to search the assembly for</typeparam>
    public static void RegisterAllTypes<T>(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Singleton
    )
    {
        var baseType = typeof(T);

        if (!baseType.IsInterface)
            throw new ArgumentOutOfRangeException(nameof(T), $"{nameof(T)} must be an interface");

        var typesFromAssemblies = assemblies.SelectMany(
            a => a.DefinedTypes.Where(x => !x.IsAbstract && x.GetInterfaces().Contains(baseType))
        );

        foreach (var type in typesFromAssemblies) {
            var serviceType = type.GetInterfaces().FirstOrDefault(interfaceType => interfaceType != baseType) ?? type;

            services.Add(new ServiceDescriptor(serviceType, type, lifetime));
        }
    }
}
