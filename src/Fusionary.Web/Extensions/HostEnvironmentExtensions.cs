using Microsoft.Extensions.Hosting;

namespace Fusionary.Web.Extensions;

public static class HostEnvironmentExtensions {
    public static bool IsLocal(this IHostEnvironment env) => env.IsEnvironment("Local");

    public static bool IsLocalOrDevelopment(this IHostEnvironment env) => env.IsDevelopment() || env.IsLocal();
}
