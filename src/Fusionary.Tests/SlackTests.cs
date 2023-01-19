using Fusionary.Slack;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using static Fusionary.Tests.TestData;

namespace Fusionary.Tests;

public class SlackIntegrationTests : TestBase
{
    public SlackIntegrationTests(ITestOutputHelper outputHelper) : base(outputHelper)
    { }

    [Fact]
    public void LogIt()
    {
        var logger = Services.GetRequiredService<ILogger<SlackIntegrationTests>>();

        using var scope = logger.BeginScope("LogIt");

        logger.LogError("LogError");
        logger.LogWarning("LogWarning");
        logger.LogCritical("LogCritical");
        logger.LogInformation("LogInformation");
        logger.LogDebug("LogDebug");
        logger.LogTrace("LogTrace");
    }

    /// <summary>
    /// Test sending a message to a Slack channel
    /// </summary>
    /// <remarks>
    /// run cli command to set secret
    /// ```
    /// dotnet user-secrets set "Slack:BotToken" "xoxb-{token}"
    /// ```
    /// https://start.1password.com/open/i?a=MTLB22HPNFB5ZBMKUZGKBZLRNM&v=3hsew7lvahgzmfxykx4h23jjde&i
    /// =uwgd76lrj5h3tdduyrhm37qsli&h=fusionary.1password.com
    /// </remarks>
    [Fact]
    public async Task Can_Send_Slack_Message_Async()
    {
        var slack = Services.GetRequiredService<ISlackNotificationService>();

        const string channelName = "monitoring-tests";
        var          headline    = $"{Faker.Hacker.Adjective()} {Faker.Hacker.Noun()} - {Faker.Internet.DomainName()}";
        var          message     = Faker.Company.CatchPhrase();
        var          jobName     = $"{Faker.Company.Bs()} {Faker.Hacker.Verb()}";
        var          error       = new ApplicationException(Faker.Hacker.Phrase());

        // lang=json
        var payload =
            $$"""
            {
              "channel": "{{channelName}}",
              "blocks": [
                {
                  "type": "header",
                  "text": {
                    "type": "plain_text",
                    "text": "{{headline}}"
                  }
                },
                {
                  "type": "divider"
                },
                {
                  "type": "section",
                  "text": {
                    "type": "mrkdwn",
                    "text": "{{message}}"
                  }
                }
              ],
              "attachments": [
                {
                  "color": "danger",
                  "author_name": "Detroit Axle",
                  "author_link": "https://www.detroitaxle.com",
                  "author_icon": "https://www.detroitaxle.com/wp-content/themes/detroit-axle/apple-touch-icon.png",
                  "title": "{{jobName}}",
                  "title_link": "https://dashboard.render.com/cron/crn-cdv4hv94reb1q3tju660/logs",
                  "text": "{{error}}",
                  "footer": "detroit-axle-wordpress-category-sync-dotnet-cron-staging",
                  "ts": {{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}}
                }
              ]
            }
            """;

        var success = await slack.PostMessageAsync(payload);

        Assert.True(success);
    }

    protected override void ConfigureServices(IServiceCollection services, IConfiguration configuration) =>
        services.AddSlack(configuration);

    protected override IConfigurationBuilder BuildConfiguration(IConfigurationBuilder builder) =>
        builder.AddUserSecrets<SlackIntegrationTests>();
}