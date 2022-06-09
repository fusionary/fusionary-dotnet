namespace Fusionary.ClientSecrets;

public class GeneratedSecrets
{
    public GeneratedSecrets(string clientId, string clientSecret, string appSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        AppSecret = appSecret;
    }

    /// <summary>
    ///     Gets the client ID.  This is public and safe to share.
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    ///     Gets the app secret.  This app secret is never shared with the client and should be stored in the database.
    /// </summary>
    public string AppSecret { get; } 

    /// <summary>
    ///     Gets the client secret.  This is a secret only shared with the client and not stored in the database.
    /// </summary>
    public string ClientSecret { get; }
}
