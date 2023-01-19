using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

namespace Fusionary.Slack;

public class SlackNotificationService : ISlackNotificationService
{
    private readonly SlackHttpClient _client;
    private readonly ILogger<SlackNotificationService> _logger;

    public SlackNotificationService(SlackHttpClient client, ILogger<SlackNotificationService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public Task<bool> PostMessageAsync(
        [StringSyntax(StringSyntaxAttribute.Json)]
        string json,
        CancellationToken cancellationToken = default
    ) => PostMessageAsync(json as object, cancellationToken);

    public async Task<bool> PostMessageAsync(object payload, CancellationToken cancellationToken)
    {
        using var scope = _logger.BeginScope("chat.postMessage");

        var response = await PostAsync("chat.postMessage", payload, cancellationToken);

        if (response.IsErrorResponse())
        {
            throw await response.ThrowSlackExceptionAsync(cancellationToken);
        }

        return await response.IsResponseOkAsync(_logger, cancellationToken);
    }

    public async Task<HttpResponseMessage> PostAsync(string path, object payload, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = payload is string json
                ? new StringContent(json)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" } }
                }
                : JsonContent.Create(payload)
        };

        return await _client.HttpClient.SendAsync(
            request,
            cancellationToken
        );
    }
}