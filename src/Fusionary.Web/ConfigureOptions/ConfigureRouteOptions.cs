using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Fusionary.Web.ConfigureOptions;

/// <summary>
/// Configure route options.
/// </summary>
public class ConfigureRouteOptions : IConfigureOptions<RouteOptions> {
    /// <inheritdoc />
    public void Configure(RouteOptions options) => options.LowercaseUrls = true;
}