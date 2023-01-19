namespace Fusionary.Slack;

public class SlackOptions
{
    public string BaseAddress { get; set; } = "https://slack.com";

    public string BotToken { get; set; } = null!;
}