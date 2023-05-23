using System.Net.Http.Headers;
using System.Text.Encodings.Web;

using Fusionary.Auth.Abstractions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Fusionary.Auth.Basic;

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthHandlerOptions>
{
    private readonly IAuthVerificationService _authVerificationService;

    public BasicAuthenticationHandler(
        IOptionsMonitor<BasicAuthHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAuthVerificationService authVerificationService
    )
        : base(options, logger, encoder, clock)
    {
        _authVerificationService = authVerificationService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            return await DoHandleAuthenticateAsync();
        }
        catch (AuthException ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    private async Task<AuthenticateResult> DoHandleAuthenticateAsync()
    {
        Request.Headers.TryGetValue("Authorization", out StringValues authorizationHeaderValues);

        AuthenticateResult result = AuthenticateResult.NoResult();

        if (authorizationHeaderValues.Count == 0)
        {
            return result;
        }

        foreach (string? value in authorizationHeaderValues)
        {
            if (
                AuthenticationHeaderValue.TryParse(value, out AuthenticationHeaderValue? headerValue)
                && string.Equals(headerValue.Scheme, BasicAuth.Scheme, StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(headerValue.Parameter)
            )
            {
                IAuthCredentials clientCredentials = BasicAuth.GetClientAuthFromValue(headerValue.Parameter);

                result = await _authVerificationService.GetAuthenticateResultAsync(
                    clientCredentials,
                    headerValue.Scheme,
                    Context.RequestAborted
                );

                if (result.Succeeded)
                {
                    return result;
                }
            }
        }

        return result;
    }
}
