using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

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

            throw new NotImplementedException();
        }
    }
}
