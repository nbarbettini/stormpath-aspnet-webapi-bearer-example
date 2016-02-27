using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using BearerAuthExample.Requests;
using BearerAuthExample.Results;
using Stormpath.SDK.Application;
using Stormpath.SDK.Error;
using Stormpath.SDK.Oauth;

namespace BearerAuthExample.Controllers
{
    [AllowAnonymous]
    public class TokenController : ApiController
    {
        private static readonly string[] SupportedGrantTypes = { "password", "refresh_token" };

        public async Task<IHttpActionResult> Post(GrantRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Grant_Type))
            {
                return new OAuthBadRequest("invalid_request", Request);
            }

            if (!SupportedGrantTypes.Contains(request.Grant_Type))
            {
                return new OAuthBadRequest("unsupported_grant_type", Request);
            }

            // Get the Stormpath application
            var application = await StormpathConfig.Client.GetApplicationAsync(StormpathConfig.ApplicationHref);

            if (!string.IsNullOrEmpty(request.Refresh_Token))
            {
                return await RefreshGrantRequest(request.Refresh_Token, application, cancellationToken);
            }
            else
            {
                return await PasswordGrantRequest(request.Username, request.Password, application, cancellationToken);
            }
        }

        private async Task<IHttpActionResult> PasswordGrantRequest(string username, string password, IApplication application, CancellationToken cancellationToken)
        {
            // Build and send a request to the Stormpath API
            var passwordGrantRequest = OauthRequests.NewPasswordGrantRequest()
                .SetLogin(username)
                .SetPassword(password)
                .Build();

            try
            {
                var result = await application.NewPasswordGrantAuthenticator()
                    .AuthenticateAsync(passwordGrantRequest, cancellationToken);

                return Ok(new TokenResult()
                {
                    AccessToken = result.AccessTokenString,
                    RefreshToken = result.RefreshTokenString,
                    TokenType = result.TokenType,
                    ExpiresIn = result.ExpiresIn
                });
            }
            catch (ResourceException rex)
            {
                return BadRequest(rex.Message);
            }
        }

        private async Task<IHttpActionResult> RefreshGrantRequest(string refresh_token, IApplication application, CancellationToken cancellationToken)
        {
            // Build and send a request to the Stormpath API
            var refreshGrantRequest = OauthRequests.NewRefreshGrantRequest()
                .SetRefreshToken(refresh_token)
                .Build();

            try
            {
                var result = await application.NewRefreshGrantAuthenticator()
                    .AuthenticateAsync(refreshGrantRequest, cancellationToken);

                return Ok(new TokenResult()
                {
                    AccessToken = result.AccessTokenString,
                    RefreshToken = result.RefreshTokenString,
                    TokenType = result.TokenType,
                    ExpiresIn = result.ExpiresIn
                });
            }
            catch (ResourceException rex)
            {
                return BadRequest(rex.Message);
            }
        }
    }
}
