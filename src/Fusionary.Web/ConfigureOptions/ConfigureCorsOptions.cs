using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Fusionary.Web.ConfigureOptions;

/// <summary>
/// Configure CORS options.
/// </summary>
public class ConfigureCorsOptions : IConfigureOptions<CorsOptions> {
    /// <summary>
    /// Defines the name of the AllowAny policy.
    /// </summary>
    public const string AllowAny = nameof(AllowAny);

    /// <inheritdoc />
    public void Configure(CorsOptions options) =>
        options.AddPolicy(
            AllowAny,
            policy => policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
        );
}