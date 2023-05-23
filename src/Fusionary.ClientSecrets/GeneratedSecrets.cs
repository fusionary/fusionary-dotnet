namespace Fusionary.ClientSecrets;

public record GeneratedSecrets
{
    /// <summary>
    ///     Gets the client ID.  This is public and safe to share.
    /// </summary>
    public required string ClientId { get; init; }

    /// <summary>
    ///     Gets the app secret.  This app secret is never shared with the client and should be stored in the database.
    /// </summary>
    public required string AppSecret { get; init; }

    /// <summary>
    ///     Gets the client secret.  This is a secret only shared with the client and not stored in the database.
    /// </summary>
    public required string ClientSecret { get; init; }
}
