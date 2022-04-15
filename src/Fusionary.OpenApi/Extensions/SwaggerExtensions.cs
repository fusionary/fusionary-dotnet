using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusionary.OpenApi.Extensions; 

public static class SwaggerExtensions {
    public static void AddXmlDocsForAssembly(this SwaggerGenOptions options, Assembly assembly) {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}

