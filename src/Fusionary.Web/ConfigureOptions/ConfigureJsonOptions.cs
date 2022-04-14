using System.Text.Json;
using System.Text.Json.Serialization;

using Fusionary.Web.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fusionary.Web.ConfigureOptions;

/// <summary>
/// Configure options for JSON serialization.
/// </summary>
public class ConfigureJsonOptions : IConfigureOptions<JsonOptions> {
    private readonly IWebHostEnvironment webHostEnvironment;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureJsonOptions" /> class.
    /// </summary>
    /// <param name="webHostEnvironment">Web Host environment.</param>
    public ConfigureJsonOptions(IWebHostEnvironment webHostEnvironment) {
        this.webHostEnvironment = webHostEnvironment;
    }

    /// <inheritdoc />
    public void Configure(JsonOptions options) {
        var jsonSerializerOptions = options.JsonSerializerOptions;
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        // Pretty print the JSON in development for easier debugging.
        jsonSerializerOptions.WriteIndented = webHostEnvironment.IsLocalOrDevelopment();
    }
}