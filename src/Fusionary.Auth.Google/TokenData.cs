using System.Text.Json.Serialization;

namespace Fusionary.Auth.Google;

public class TokenData {
    private string[]? _roles;

    [JsonPropertyName("e")]
    public string Email { get; set; } = "";

    [JsonPropertyName("n")]
    public string Name { get; set; } = "";


    [JsonPropertyName("r")]
    public string[] Roles
    {
        get => _roles ?? Array.Empty<string>();
        set => _roles = value;
    }

    [JsonPropertyName("u")]
    public string UserID { get; set; } = "";
}
