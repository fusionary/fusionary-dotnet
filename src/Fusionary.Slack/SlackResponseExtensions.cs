using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Fusionary.Slack;

internal static class SlackResponseExtensions
{
    public static bool IsErrorResponse(this HttpResponseMessage response) =>
        response.IsSuccessStatusCode == false ||
        response is { Content: null } ||
        response.Content.Headers.ContentType?.MediaType != "application/json";

    public static Task<bool> IsResponseOkAsync(
        this HttpResponseMessage response,
        CancellationToken cancellationToken
    ) =>
        IsResponseOkAsync(response, NullLogger.Instance, cancellationToken);

    public static async Task<bool> IsResponseOkAsync(
        this HttpResponseMessage response,
        ILogger logger,
        CancellationToken cancellationToken
    )
    {
        var responseJson = await response.Content.ReadFromJsonAsync<JsonDocument>(cancellationToken: cancellationToken);

        logger.LogInformation("Slack response: {Response}", responseJson!.RootElement.ToString());

        responseJson!.RootElement.TryGetProperty("ok", out var ok);

        return ok.GetBoolean();
    }

    public static async Task<SlackException> ThrowSlackExceptionAsync(
        this HttpResponseMessage response,
        CancellationToken cancellationToken
    )
    {
        var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

        return new SlackException(responseText, null, response.StatusCode);
    }
}