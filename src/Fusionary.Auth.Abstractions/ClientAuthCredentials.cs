namespace Fusionary.Auth.Abstractions;

public record ClientAuthCredentials
{
    public required string Id { get; init; }

    public required string Secret { get; init; }
}