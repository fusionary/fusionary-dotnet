namespace Fusionary.Auth.Google;

using System.Text.Json.Serialization;

public class TokenData
{
    private string[]? _roles;

    [JsonPropertyName("e")] public string Email { get; set; } = "";

    [JsonPropertyName("n")] public string Name { get; set; } = "";


    [JsonPropertyName("r")]
    public string[] Roles
    {
        get => this._roles ?? Array.Empty<string>();
        set => this._roles = value;
    }

    [JsonPropertyName("u")] public string UserID { get; set; }
}
