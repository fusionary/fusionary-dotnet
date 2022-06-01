namespace Fusionary.Auth.Google;

using System.ComponentModel.DataAnnotations;

public class GoogleAuthInput
{
    [Required] public string TokenId { get; set; } = "";
}
