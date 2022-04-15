using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace Fusionary.Web.Extensions;

public static class ClaimsExtensions {
    /// <summary>
    /// The email for the user
    /// </summary>
    public static string Email(this ClaimsPrincipal? user) {
        return user?.FindFirstValue(ClaimTypes.Email) ?? "";
    }

    /// <summary>
    /// The name for the user
    /// </summary>
    public static string Name(this ClaimsPrincipal? user) {
        return user?.FindFirstValue(ClaimTypes.Name) ?? "";
    }

    /// <summary>
    /// The mobile phone for the user
    /// </summary>
    public static string MobilePhone(this ClaimsPrincipal? user) {
        return user?.FindFirstValue(ClaimTypes.MobilePhone) ?? "";
    }
    
    /// <summary>
    /// The home phone for the user
    /// </summary>
    public static string HomePhone(this ClaimsPrincipal? user) {
        return user?.FindFirstValue(ClaimTypes.HomePhone) ?? "";
    }

    /// <summary>
    /// The currently authorized user
    /// </summary>
    public static ClaimsPrincipal? User(this IHttpContextAccessor httpContextAccessor) {
        return httpContextAccessor.HttpContext?.User;
    }
    
    /// <summary>
    /// Returns the value for the first claim of the specified type, otherwise null if the claim is not present.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance this method extends.</param>
    /// <param name="claimType">The claim type whose first value should be returned.</param>
    /// <returns>The value of the first instance of the specified claim type, or null if the claim is not present.</returns>
    private static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        var claim = principal.FindFirst(claimType);
        return claim?.Value ?? string.Empty;
    }
}
