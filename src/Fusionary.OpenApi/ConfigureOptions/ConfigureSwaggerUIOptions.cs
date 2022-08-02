using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace Fusionary.OpenApi.ConfigureOptions;

/// <summary>
///     Configure Swagger UI options.
/// </summary>
public class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions> {
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;
    private readonly IWebHostEnvironment env;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigureSwaggerUIOptions" /> class.
    /// </summary>
    /// <param name="apiVersionDescriptionProvider">Api version description provider.</param>
    /// <param name="env">Environment information</param>
    public ConfigureSwaggerUIOptions(
        IApiVersionDescriptionProvider apiVersionDescriptionProvider,
        IWebHostEnvironment env
    )
    {
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        this.env = env;
    }


    /// <inheritdoc />
    public void Configure(SwaggerUIOptions options)
    {
        options.DocumentTitle = env.ApplicationName;

        options.DisplayOperationId();
        options.DisplayRequestDuration();

        foreach (var apiVersionDescription in apiVersionDescriptionProvider
                     .ApiVersionDescriptions
                     .OrderByDescending(x => x.ApiVersion))
            options.SwaggerEndpoint(
                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                $"Version {apiVersionDescription.ApiVersion}"
            );
    }
}
