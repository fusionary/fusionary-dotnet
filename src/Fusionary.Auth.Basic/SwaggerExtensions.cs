using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusionary.Auth.Basic;

public static class SwaggerExtensions
{
    public static void AddBasicAuth(this SwaggerGenOptions options)
    {
        OpenApiSecurityScheme basicScheme = new OpenApiSecurityScheme
        {
            Name = "Basic Authentication",
            Description = "Enter the basic auth credentials",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            Reference = new OpenApiReference { Id = BasicAuth.Scheme, Type = ReferenceType.SecurityScheme }
        };

        options.AddSecurityDefinition(basicScheme.Reference.Id, basicScheme);

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = BasicAuth.Scheme, Type = ReferenceType.SecurityScheme
                        }
                    },
                    new[] { "readAccess", "writeAccess" }
                }
            }
        );
    }
}
