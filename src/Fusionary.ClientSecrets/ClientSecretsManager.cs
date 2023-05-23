using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Fusionary.ClientSecrets;

public static class ClientSecretsManager {
    private const int SaltSize = 16;

    /// <summary>
    ///     Creates an app secret that can be used to verify a client secret.
    /// </summary>
    /// <param name="clientSecret">The client secret.</param>
    /// <returns>A base64 encoded string.</returns>
    public static string CreateAppSecret(string clientSecret)
    {
        var randomSaltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        var hashBytes = ComputeSaltedHash(clientSecret, randomSaltBytes);

        return Convert.ToBase64String(Combine(randomSaltBytes, hashBytes));
    }

    /// <summary>
    ///     Verifies the <paramref name="clientSecret" /> using the <paramref name="appSecret" />.
    /// </summary>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="appSecret">The app secret.</param>
    /// <returns><c>true</c> if the client secret is valid for the app secret; otherwise <c>false</c>.</returns>
    public static bool VerifyClientSecret(string clientSecret, string appSecret)
    {
        var hashedValueBytes = Convert.FromBase64String(appSecret);

        var saltBytes   = hashedValueBytes[Range.EndAt(SaltSize)];
        var hashedBytes = hashedValueBytes[Range.StartAt(SaltSize)];

        var checkedBytes = ComputeSaltedHash(clientSecret, saltBytes);

        return StructuralComparisons.StructuralEqualityComparer.Equals(checkedBytes, hashedBytes);
    }

    /// <summary>
    ///     Generates new client credential secrets.
    /// </summary>
    /// <returns>An instance of <see cref="GenerateSecrets" />.</returns>
    public static GeneratedSecrets GenerateSecrets()
    {
        var clientId     = GenerateSecret();
        var clientSecret = GenerateSecret();
        var appSecret    = CreateAppSecret(clientSecret);

        return new GeneratedSecrets { ClientId = clientId, ClientSecret = clientSecret, AppSecret = appSecret };
    }

    private static string GenerateSecret()
    {
        return $"{Guid.NewGuid():n}";
    }

    private static byte[] ComputeSaltedHash(string value, byte[] saltBytes)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(value);

        return ComputeHash(saltBytes, plainTextBytes);
    }

    private static byte[] ComputeHash(byte[] value, byte[] salt)
    {
        return HMACSHA256.HashData(salt, value);
    }

    private static byte[] Combine(byte[] first, byte[] second)
    {
        var ret = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
        return ret;
    }
}
