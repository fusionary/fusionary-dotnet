using Fusionary.Core;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace Fusionary.OpenApi.ConfigureOptions;

/// <summary>
/// Configure Swagger UI options.
/// </summary>
public class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions> {
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerUIOptions" /> class.
    /// </summary>
    /// <param name="apiVersionDescriptionProvider">Api version description provider.</param>
    public ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) {
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }


    /// <inheritdoc />
    public void Configure(SwaggerUIOptions options) {
        options.DocumentTitle = AssemblyInformation.Current.Product;

        options.DisplayOperationId();
        options.DisplayRequestDuration();

        foreach (var apiVersionDescription in apiVersionDescriptionProvider
                     .ApiVersionDescriptions
                     .OrderByDescending(x => x.ApiVersion)) {
            options.SwaggerEndpoint(
                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                $"Version {apiVersionDescription.ApiVersion}"
            );
        }
    }
}