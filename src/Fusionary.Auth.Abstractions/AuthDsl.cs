using System.Security.Claims;

namespace Fusionary.Auth.Abstractions;

public static class AuthDsl
{
    public static bool IsAuthenticated(this ClaimsPrincipal? user)
    {
        return user?.Identity != null && user.Identity.IsAuthenticated;
    }
}