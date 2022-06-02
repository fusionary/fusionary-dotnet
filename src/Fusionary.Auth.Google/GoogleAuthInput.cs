using System.ComponentModel.DataAnnotations;

namespace Fusionary.Auth.Google;

public class GoogleAuthInput {
    [Required]
    public string TokenId { get; set; } = "";
}
