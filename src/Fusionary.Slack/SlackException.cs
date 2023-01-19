using System.Net;

namespace Fusionary.Slack;

public class SlackException : HttpRequestException
{
    public SlackException()
    { }

    public SlackException(string message) : base(message)
    { }

    public SlackException(string message, Exception inner) : base(message, inner)
    { }

    public SlackException(string? message, Exception? inner, HttpStatusCode? statusCode) : base(
        message,
        inner,
        statusCode
    )
    { }
}