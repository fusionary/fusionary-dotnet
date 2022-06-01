namespace Fusionary.Auth.Google;

using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

public class TokenService
{
    private readonly ITimeLimitedDataProtector _protector;

    public TokenService(IDataProtectionProvider protectionProvider)
    {
        this._protector = protectionProvider.CreateProtector("authToken").ToTimeLimitedDataProtector();
    }

    public string Protect(TokenData data, TimeSpan expiresIn)
    {
        return this._protector.Protect(JsonSerializer.Serialize(data), expiresIn);
    }

    public TokenData? Unprotect(string protectedData)
    {
        try {
            return JsonSerializer.Deserialize<TokenData>(this._protector.Unprotect(protectedData));
        }
        catch (CryptographicException) {
            throw new AuthException("Authorization is invalid or expired");
        }
    }
}
