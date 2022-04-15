using System.Security.Cryptography;
using System.Text;

namespace Fusionary.Core.Extensions;

public static class RandomString {
    /// <summary>
    /// Creates a cryptographically strong sequence of characters.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <param name="allowedChars">The allowed chars.</param>
    /// <returns>A random string</returns>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="length" /> may not be less than zero</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="allowedChars" /> may not be empty</exception>
    /// <exception cref="ArgumentException">The <paramref name="allowedChars" /> may not contain more than 256 characters</exception>
    public static string Create(int length, string allowedChars = "abcdefghjklmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789") {
        if (length < 0) {
            throw new ArgumentOutOfRangeException(nameof(length), $"{nameof(length)} cannot be less than zero.");
        }

        if (string.IsNullOrEmpty(allowedChars)) {
            throw new ArgumentNullException(nameof(allowedChars), $"{nameof(allowedChars)} may not be empty.");
        }

        const int byteSize = 0x100;

        var allowedCharSet = new HashSet<char>(allowedChars).ToArray();

        if (byteSize < allowedCharSet.Length) {
            throw new ArgumentException($"{nameof(allowedChars)} may contain no more than {byteSize} characters.");
        }

        using var rng    = RandomNumberGenerator.Create();
        var       result = new StringBuilder();
        var       buf    = new byte[128];

        while (result.Length < length) {
            rng.GetBytes(buf);

            for (var i = 0; i < buf.Length && result.Length < length; ++i) {
                var outOfRangeStart = byteSize - byteSize % allowedCharSet.Length;
                if (outOfRangeStart <= buf[i]) {
                    continue;
                }

                result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
            }
        }

        return result.ToString();
    }
}
