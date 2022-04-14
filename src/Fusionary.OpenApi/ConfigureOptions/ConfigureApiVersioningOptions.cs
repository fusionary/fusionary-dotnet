using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace Fusionary.OpenApi.ConfigureOptions;

/// <summary>
/// Configuration options for API versioning.
/// </summary>
public class ConfigureApiVersioningOptions :
    IConfigureOptions<ApiVersioningOptions>,
    IConfigureOptions<ApiExplorerOptions> {
    /// <inheritdoc />
    public void Configure(ApiExplorerOptions options) => options.GroupNameFormat = "'v'VVV";

    /// <inheritdoc />
    public void Configure(ApiVersioningOptions options) {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    }
}