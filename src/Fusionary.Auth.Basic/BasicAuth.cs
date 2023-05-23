using System.Text;

namespace Fusionary.Auth.Basic;

public static class BasicAuth
{
    public const string Scheme = "Basic";

    public static IAuthCredentials GetClientAuthFromValue(string value)
    {
        byte[] bytes = Convert.FromBase64String(value);

        string stringValue = Encoding.UTF8.GetString(bytes);

        string[] parts = stringValue.Split(':', StringSplitOptions.RemoveEmptyEntries);

        string username = parts.ElementAtOrDefault(0) ?? "";
        string password = parts.ElementAtOrDefault(1) ?? "";

        return new AuthCredentials { Username = username, Password = password };
    }

    public static string ToBasicAuth(IAuthCredentials credentials)
    {
        string value = $"{credentials.Username}:{credentials.Password}";

        byte[] bytes = Encoding.UTF8.GetBytes(value);

        return Convert.ToBase64String(bytes);
    }
}
