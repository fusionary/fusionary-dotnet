using System.Net.Http.Headers;

using Microsoft.Extensions.Options;

namespace Fusionary.Slack;

public class SlackHttpClient
{
    public SlackHttpClient(HttpClient client, IOptions<SlackOptions> options)
    {
        var config = options.Value;

        client.BaseAddress = new UriBuilder(config.BaseAddress) { Path = "/api/" }.Uri;

        var headers = client.DefaultRequestHeaders;

        headers.Authorization = new AuthenticationHeaderValue("Bearer", config.BotToken);
        headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json") { CharSet = "utf-8" });

        HttpClient = client;
    }


    public HttpClient HttpClient { get; }
}