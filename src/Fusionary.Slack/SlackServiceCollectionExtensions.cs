using System.Net;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.Slack;

public static class SlackServiceCollectionExtensions
{
    public static IServiceCollection AddSlack(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SlackOptions>(configuration.GetSection("Slack"));
        services.AddHttpClient<SlackHttpClient>()
            .ConfigurePrimaryHttpMessageHandler(
                () => new HttpClientHandler
                {
                    AllowAutoRedirect = false, UseCookies = false, AutomaticDecompression = DecompressionMethods.All
                }
            );
        services.AddSingleton<ISlackNotificationService, SlackNotificationService>();

        return services;
    }
}