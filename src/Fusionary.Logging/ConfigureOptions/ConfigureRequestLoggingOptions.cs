using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;

namespace Fusionary.Logging.ConfigureOptions;

/// <summary>
/// Configure request logging.
/// </summary>
public class ConfigureRequestLoggingOptions : IConfigureOptions<RequestLoggingOptions> {
    private const string ContentTypePropertyName = "ContentType";
    private const string EndpointNamePropertyName = "EndpointName";

    private const string HealthCheckEndpointDisplayName = "Health checks";

    private const string HostPropertyName = "Host";

    private const string MessageTemplate = "{Protocol} {RequestMethod} {RequestPath} responded {StatusCode} {ContentType} in {Elapsed:0.0000} ms";

    private const string ProtocolPropertyName = "Protocol";
    private const string QueryStringPropertyName = "QueryString";
    private const string SchemePropertyName = "Scheme";

    /// <inheritdoc />
    public void Configure(RequestLoggingOptions options) {
        options.EnrichDiagnosticContext = EnrichDiagnosticContext;
        options.GetLevel = GetLevel;
        options.MessageTemplate = MessageTemplate;
    }

    private static void EnrichDiagnosticContext(IDiagnosticContext diagnosticContext, HttpContext httpContext) {
        var request  = httpContext.Request;
        var response = httpContext.Response;

        diagnosticContext.Set(HostPropertyName, request.Host);
        diagnosticContext.Set(ProtocolPropertyName, request.Protocol);
        diagnosticContext.Set(SchemePropertyName, request.Scheme);

        var queryString = request.QueryString;
        if (queryString.HasValue) {
            diagnosticContext.Set(QueryStringPropertyName, queryString.Value);
        }

        var endpoint = httpContext.GetEndpoint();
        if (endpoint is not null) {
            diagnosticContext.Set(EndpointNamePropertyName, endpoint.DisplayName);
        }

        diagnosticContext.Set(ContentTypePropertyName, response.ContentType);
    }

    private static LogEventLevel GetLevel(HttpContext httpContext, double elapsedMilliseconds, Exception? exception) {
        if (exception is not null || httpContext.Response.StatusCode > 499) {
            return LogEventLevel.Error;
        }
        
        return IsHealthCheckEndpoint(httpContext) ? LogEventLevel.Verbose : LogEventLevel.Information;
    }

    private static bool IsHealthCheckEndpoint(HttpContext httpContext) {
        var endpoint = httpContext.GetEndpoint();
        return endpoint is not null && string.Equals(endpoint.DisplayName, HealthCheckEndpointDisplayName, StringComparison.Ordinal);
    }
}