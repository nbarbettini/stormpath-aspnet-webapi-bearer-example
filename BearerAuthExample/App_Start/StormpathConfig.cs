using Stormpath.SDK.Client;
using Stormpath.SDK.Http;
using Stormpath.SDK.Serialization;
using Stormpath.SDK.Sync;

namespace BearerAuthExample
{
    public static class StormpathConfig
    {
        // Replace this with your Stormpath Application href (found in the Stormpath Admin Console)
        public static readonly string ApplicationHref = "https://api.stormpath.com/v1/applications/7Ol377HU068lagCYk7U9XS";

        // This static object will allow us to use the Stormpath client object elsewhere in the application
        public static IClient Client { get; private set; }

        public static void Initialize()
        {
            // Build the IClient object and make it available on StormpathConfig.Client.
            // (In a complex application, it would be better to use dependency injection to do this,
            // instead of a singleton/static object)
            var clientBuilder = Clients.Builder()
                // Path to your API Key file path. You can also hardcode the API Key and Secret
                // with .SetApiKeyId/SetApiKeySecret, but this should only be used for testing.
                .SetApiKeyFilePath("~\\.stormpath\\useful-catapult.apiKey.properties")
                .SetHttpClient(HttpClients.Create().RestSharpClient())
                .SetSerializer(Serializers.Create().JsonNetSerializer());

            Client = clientBuilder.Build();
            Client.GetApplication(ApplicationHref); // Prime the cache
        }
    }
}