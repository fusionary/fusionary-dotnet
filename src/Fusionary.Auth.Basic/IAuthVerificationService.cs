using Microsoft.AspNetCore.Authentication;

namespace Fusionary.Auth.Basic;

public interface IAuthVerificationService
{
    Task<AuthenticateResult> GetAuthenticateResultAsync(
        IAuthCredentials credentials,
        string scheme,
        CancellationToken cancellationToken
    );
}
