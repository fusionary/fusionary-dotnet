using Fusionary.Core;

using Google.Apis.Auth;

namespace Fusionary.Auth.Google;

public class TokenLoginService {
    private readonly TokenService tokenService;

    public TokenLoginService(TokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public async Task<Result<AuthResult>> GoogleLoginAsync(
        GoogleAuthInput input,
        CancellationToken cancellationToken
    )
    {
        try {
            var payload = await GoogleJsonWebSignature.ValidateAsync(input.TokenId);
            
            var expires = payload.ExpirationTimeSeconds.HasValue
                ? DateTimeOffset.FromUnixTimeSeconds(payload.ExpirationTimeSeconds.Value) - DateTimeOffset.Now
                : TimeSpan.FromDays(1.0);
      
            return await CreateAuthResultAsync(GetUserInfo(payload), expires.TotalSeconds, cancellationToken);
        } catch (InvalidJwtException) {
            return Result.Fail<AuthResult>("Token not valid");
        }
    }

    private GoogleUserInfo GetUserInfo(GoogleJsonWebSignature.Payload payload)
    {
        return new GoogleUserInfo() {
            EmailAddress = payload.Email,
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            ImageUrl = payload.Picture
        };
    }

    private Task<Result<AuthResult>> CreateAuthResultAsync(
        GoogleUserInfo user,
        double expiresInSeconds,
        CancellationToken cancellationToken = default
    )
    {
        var userRoles = GetUserRoles(user.EmailAddress);
        var str = tokenService.Protect(
            new TokenData {
                UserID = user.EmailAddress,
                Name = user.FullName(),
                Email = user.EmailAddress,
                Roles = userRoles
            },
            TimeSpan.FromSeconds(expiresInSeconds)
        );
        return Task.FromResult(
            Result.Ok(
                new AuthResult {
                    Token = str,
                    Name = user.FullName(),
                    Initials = user.Initials(),
                    Email = user.EmailAddress,
                    ProfileImage = user.ImageUrl,
                    ExpiresIn = Convert.ToInt32(expiresInSeconds),
                    Roles = userRoles
                }
            )
        );
    }

    private string[] GetUserRoles(string email)
    {
        return Array.Empty<string>();
    }
}
