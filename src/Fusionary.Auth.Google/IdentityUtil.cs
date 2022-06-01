namespace Fusionary.Auth.Google;

using System.Security.Claims;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public static class IdentityUtil
{
    public static ClaimsIdentity CreateIdentity(this TokenData tokenData, string? authenticationType = null)
    {
        var claims = CreateClaims(tokenData);

        var authType = string.IsNullOrEmpty(authenticationType)
            ? JwtBearerDefaults.AuthenticationScheme
            : authenticationType;

        return new ClaimsIdentity(claims, authType, ClaimTypes.NameIdentifier, ClaimTypes.Role);
    }

    public static ClaimsPrincipal CreatePrincipal(this TokenData tokenData)
    {
        var identity = tokenData.CreateIdentity();

        return new ClaimsPrincipal(identity);
    }

    private static IEnumerable<Claim> CreateClaims(TokenData tokenData)
    {
        var claims = new List<Claim> {
                                         { ClaimTypes.NameIdentifier, tokenData.UserID },
                                         { ClaimTypes.Name, tokenData.Name },
                                         { ClaimTypes.Email, tokenData.Email }
                                     };

        claims.AddRange(tokenData.Roles.Select(roleName => new Claim(ClaimTypes.Role, roleName)));

        return claims;
    }
}
