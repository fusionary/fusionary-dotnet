using Fusionary.Auth.Abstractions;

namespace Fusionary.Auth.Basic;

public class BasicAuthException : AuthException
{
    public BasicAuthException(string message) : base(message) { }
}
