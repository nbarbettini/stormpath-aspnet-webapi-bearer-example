using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Stormpath.SDK.Error;
using Stormpath.SDK.Oauth;

namespace BearerAuthExample.Filters
{
    public class StormpathBearerAuthenticationAttribute : BearerAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(string token, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            // Get the Stormpath application
            var application = await StormpathConfig.Client.GetApplicationAsync(StormpathConfig.ApplicationHref);

            // Build and send a request to the Stormpath API
            var jwtValidationRequest = OauthRequests.NewJwtAuthenticationRequest()
                .SetJwt(token)
                .Build();
            
            try
            {
                var validationResult = await application.NewJwtAuthenticator()
                    .AuthenticateAsync(jwtValidationRequest, cancellationToken);

                var accountDetails = await validationResult.GetAccountAsync();

                // Build an IPrincipal and return it
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, accountDetails.Email));
                claims.Add(new Claim(ClaimTypes.GivenName, accountDetails.GivenName));
                claims.Add(new Claim(ClaimTypes.Surname, accountDetails.Surname));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, accountDetails.Username));
                var identity = new ClaimsIdentity(claims, AuthenticationTypes.Signature);
                return new ClaimsPrincipal(identity);
            }
            catch (ResourceException rex)
            {
                return null;
            }
        }
    }
}
