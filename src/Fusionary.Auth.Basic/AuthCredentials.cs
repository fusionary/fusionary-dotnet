namespace Fusionary.Auth.Basic;

public record AuthCredentials : IAuthCredentials
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}
