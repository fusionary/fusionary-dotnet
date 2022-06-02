using System.Security.Cryptography;
using System.Text.Json;

using Microsoft.AspNetCore.DataProtection;

namespace Fusionary.Auth.Google;

public class TokenService {
    private readonly ITimeLimitedDataProtector _protector;

    public TokenService(IDataProtectionProvider protectionProvider)
    {
        _protector = protectionProvider.CreateProtector("authToken").ToTimeLimitedDataProtector();
    }

    public string Protect(TokenData data, TimeSpan expiresIn)
    {
        return _protector.Protect(JsonSerializer.Serialize(data), expiresIn);
    }

    public TokenData? Unprotect(string protectedData)
    {
        try {
            return JsonSerializer.Deserialize<TokenData>(_protector.Unprotect(protectedData));
        } catch (CryptographicException) {
            throw new AuthException("Authorization is invalid or expired");
        }
    }
}
