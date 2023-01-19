using System.Diagnostics.CodeAnalysis;

namespace Fusionary.Slack;

public interface ISlackNotificationService
{
    Task<bool> PostMessageAsync(object payload, CancellationToken cancellationToken = default);

    Task<bool> PostMessageAsync(
        [StringSyntax(StringSyntaxAttribute.Json)]
        string json,
        CancellationToken cancellationToken = default
    );
}