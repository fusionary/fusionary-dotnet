using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Options;

namespace Fusionary.Web.ConfigureOptions;

/// <summary>
/// Configure HSTS options.
/// </summary>
public class ConfigureHstsOptions : IConfigureOptions<HstsOptions> {
    private static readonly TimeSpan DefaultPolicy = TimeSpan.FromDays(365);

    /// <inheritdoc />
    public void Configure(HstsOptions options) {
        options.IncludeSubDomains = true;
        options.MaxAge = DefaultPolicy;
        options.Preload = true;
    }
}