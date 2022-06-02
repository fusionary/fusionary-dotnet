using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fusionary.Auth.Google;

public class AuthResult {
    private string[]? _roles;

    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [Required]
    [JsonPropertyName("initials")]
    public string Initials { get; set; } = "";

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }

    [Required]
    [JsonPropertyName("token")]
    public string Token { get; set; } = "";

    [Required]
    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; set; }

    [Required]
    [JsonPropertyName("roles")]
    public string[] Roles
    {
        get => LazyInitializer.EnsureInitialized(ref _roles, Array.Empty<string>);
        set => _roles = value;
    }
}
