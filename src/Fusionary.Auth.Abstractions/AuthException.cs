namespace Fusionary.Auth.Abstractions;

public class AuthException : ApplicationException
{
    public AuthException(string message) : base(message) { }
}
