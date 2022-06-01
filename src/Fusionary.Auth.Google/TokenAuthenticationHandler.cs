namespace Fusionary.Auth.Google;

using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationHandlerOptions>
{
    private readonly TokenService _tokenService;

    public TokenAuthenticationHandler(
        IOptionsMonitor<TokenAuthenticationHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        TokenService tokenService)
        : base(options, logger, encoder, clock)
    {
        this._tokenService = tokenService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try {
            return await this.DoHandleAuthenticateAsync();
        }
        catch (AuthException ex) {
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    private static AuthenticationTicket CreateAuthenticationTicket(TokenData tokenData)
    {
        var principal = tokenData.CreatePrincipal();

        var props = new AuthenticationProperties();

        return new AuthenticationTicket(principal, props, JwtBearerDefaults.AuthenticationScheme);
    }

    private Task<AuthenticateResult> DoHandleAuthenticateAsync()
    {
        this.Request.Headers.TryGetValue("authorization", out var authorizationHeaderValues);
        this.Request.Query.TryGetValue("auth_token", out var queryStringValues);

        var result = AuthenticateResult.NoResult();

        if (authorizationHeaderValues.Count > 0) {
            foreach (var value in authorizationHeaderValues) {
                if (
                    AuthenticationHeaderValue.TryParse(value, out var headerValue)
                    && headerValue.Scheme.Equals(JwtBearerDefaults.AuthenticationScheme,
                        StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrWhiteSpace(headerValue.Parameter)
                ) {
                    result = this.GetAuthenticateResult(headerValue.Parameter);
                }
            }
        }
        else if (queryStringValues.Count > 0) {
            var value = queryStringValues.First();

            if (!string.IsNullOrWhiteSpace(value)) {
                result = this.GetAuthenticateResult(value);
            }
        }

        return Task.FromResult(result);
    }

    private AuthenticateResult GetAuthenticateResult(string value)
    {
        var tokenData = this._tokenService.Unprotect(value);

        if (tokenData == null) {
            return AuthenticateResult.NoResult();
        }

        var ticket = CreateAuthenticationTicket(tokenData);

        return AuthenticateResult.Success(ticket);
    }
}
