namespace Fusionary.Auth.Google;

using Fusionary.Core;
using global::Google.Apis.Auth;

public class TokenLoginService
{
    private readonly TokenService tokenService;

    public TokenLoginService(TokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public async Task<Result<AuthResult>> GoogleLoginAsync(GoogleAuthInput input, CancellationToken cancellationToken)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(input.TokenId);

            var user = this.GetUserInfo(payload);

            var expiresIn = Convert.ToInt32(payload.ExpirationTimeSeconds ?? TimeSpan.FromDays(1).TotalSeconds);

            return await this.CreateAuthResultAsync(user, expiresIn, cancellationToken);
        }
        catch (InvalidJwtException) {
            return Result.Fail<AuthResult>("Token not valid");
        }
    }

    private GoogleUserInfo GetUserInfo(GoogleJsonWebSignature.Payload payload)
    {
        return new GoogleUserInfo {
                                      EmailAddress = payload.Email,
                                      FirstName = payload.GivenName,
                                      LastName = payload.FamilyName,
                                      ImageUrl = payload.Picture
                                  };
    }

    private Task<Result<AuthResult>> CreateAuthResultAsync(
        GoogleUserInfo user,
        int expiresInSeconds,
        CancellationToken cancellationToken = default)
    {
        var roles = this.GetUserRoles(user.EmailAddress);

        var tokenData = new TokenData {
                                          UserID = user.EmailAddress,
                                          Name = user.FullName(),
                                          Email = user.EmailAddress,
                                          Roles = roles
                                      };

        var token = this.tokenService.Protect(tokenData, TimeSpan.FromSeconds(expiresInSeconds));

        var authResult = new AuthResult {
                                            Token = token,
                                            Name = user.FullName(),
                                            Initials = user.Initials(),
                                            Email = user.EmailAddress,
                                            ProfileImage = user.ImageUrl,
                                            ExpiresIn = expiresInSeconds,
                                            Roles = roles
                                        };

        var result = Result.Ok(authResult);

        return Task.FromResult(result);
    }

    private string[] GetUserRoles(string email)
    {
        return Array.Empty<string>();
    }
}
