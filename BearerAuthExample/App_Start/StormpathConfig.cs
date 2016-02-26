using Stormpath.SDK.Api;
using Stormpath.SDK.Client;
using Stormpath.SDK.Http;
using Stormpath.SDK.Serialization;

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
            var apiKey = ClientApiKeys.Builder()
                // Replace this with your Stormpath API Key and Secret
                // or use SetFileLocation and provide a path to apiKey.properties
                .SetFileLocation("~\\.stormpath\\useful-catapult.apiKey.properties")
                .Build();

            // Build the IClient object and make it available on StormpathConfig.Client
            // In a complex application, it would be better to use dependency injection to do this.
            var clientBuilder = Clients.Builder()
                .SetApiKey(apiKey)
                .SetHttpClient(HttpClients.Create().RestSharpClient())
                .SetSerializer(Serializers.Create().JsonNetSerializer());

            Client = clientBuilder.Build();
        }
    }
}