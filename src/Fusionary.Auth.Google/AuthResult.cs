namespace Fusionary.Auth.Google;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AuthResult
{
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
    public string[] Roles { get; set; }
}
