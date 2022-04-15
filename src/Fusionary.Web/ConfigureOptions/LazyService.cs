using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.Web.ConfigureOptions;

public class LazyService<T> : Lazy<T> where T : class {
    public LazyService(IServiceProvider serviceProvider)
        : base(serviceProvider.GetRequiredService<T>) {
    }
}