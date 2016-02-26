using System.Web.Http;
using BearerAuthExample.Filters;

namespace BearerAuthExample.Controllers
{
    [StormpathBearerAuthentication]
    [Authorize]
    public class ProtectedController : ApiController
    {
        public string Get()
        {
            return "hello secure world!";
        }
    }
}
