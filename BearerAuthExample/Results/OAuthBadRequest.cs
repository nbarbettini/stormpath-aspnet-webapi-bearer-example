using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace BearerAuthExample.Results
{
    public class OAuthBadRequest : IHttpActionResult
    {
        public OAuthBadRequest(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.RequestMessage = Request;
            response.Content = new ObjectContent<OAuthError>(new OAuthError(ReasonPhrase), new JsonMediaTypeFormatter());

            return Task.FromResult(response);
        }
    }

    public class OAuthError
    {
        public OAuthError(string error)
        {
            Error = error;
        }

        [JsonProperty("error")]
        public string Error { get; private set; }
    }
}
