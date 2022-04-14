using System.Reflection;

using Fusionary.Core;
using Fusionary.OpenApi.OperationFilters;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusionary.OpenApi.ConfigureOptions;

/// <summary>
/// Configure Swagger gen options.
/// </summary>
public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions> {
    private readonly IApiVersionDescriptionProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions" /> class.
    /// </summary>
    /// <param name="provider">Api version description provider.</param>
    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider) {
        this.provider = provider;
    }


    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options) {
        options.DescribeAllParametersInCamelCase();
        options.EnableAnnotations();

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

        options.OperationFilter<ApiVersionOperationFilter>();

        foreach (var apiVersionDescription in provider.ApiVersionDescriptions) {
            var info = new OpenApiInfo
                       {
                           Title = AssemblyInformation.Current.Product,
                           Description =
                               apiVersionDescription.IsDeprecated
                                   ? $"{AssemblyInformation.Current.Description} This API version has been deprecated."
                                   : AssemblyInformation.Current.Description,
                           Version = apiVersionDescription.ApiVersion.ToString(),
                       };
            options.SwaggerDoc(apiVersionDescription.GroupName, info);
        }
    }
}