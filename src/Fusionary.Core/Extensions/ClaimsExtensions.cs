using System.Security.Claims;

namespace Fusionary.Core.Extensions; 

public static class ClaimsExtensions {
    public static void Add(this ICollection<Claim> claims, string type, object value) {
        claims?.Add(new Claim(type, Convert.ToString(value) ?? ""));
    }

    public static string[] GetRoleNames(this ClaimsPrincipal? user) {
        return user?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray() ?? Array.Empty<string>();
    }
    
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
    /// Returns the value for the first claim of the specified type, otherwise null if the claim is not present.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance this method extends.</param>
    /// <param name="claimType">The claim type whose first value should be returned.</param>
    /// <returns>The value of the first instance of the specified claim type, or null if the claim is not present.</returns>
    private static string FindFirstValue(this ClaimsPrincipal? principal, string claimType)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        var claim = principal.FindFirst(claimType);
        return claim?.Value ?? string.Empty;
    }
}