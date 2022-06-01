namespace Fusionary.Auth.Google;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var requiredScopes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Select(attr => attr.Policy)
            .Distinct()
            .ToArray();

        if (!requiredScopes.Any())
        {
            return;
        }

        operation.Responses.Add("401", new OpenApiResponse
                                       {
                                           Description = "Unauthorized",
                                       });

        var oAuthScheme = new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                                          {
                                              Id = JwtBearerDefaults.AuthenticationScheme,
                                              Type = ReferenceType.SecurityScheme,
                                          },
                          };

        operation.Security = new List<OpenApiSecurityRequirement>
                             {
                                 new()
                                 {
                                     [oAuthScheme] = requiredScopes.ToList(),
                                 },
                             };
    }
}
