using System.Security.Claims;

namespace Fusionary.Auth.Abstractions;

public static class IdentityUtil
{
    public static ClaimsPrincipal CreatePrincipal(this IEnumerable<Claim> claims, string scheme)
    {
        ClaimsIdentity identity = claims.CreateIdentity(scheme);

        return new ClaimsPrincipal(identity);
    }

    private static ClaimsIdentity CreateIdentity(this IEnumerable<Claim> claims, string scheme)
    {
        return new ClaimsIdentity(claims, scheme, ClaimTypes.NameIdentifier, ClaimTypes.Role);
    }
}