namespace Fusionary.Auth.Basic;

public interface IAuthCredentials
{
    string Username { get; }
    string Password { get; }
}
