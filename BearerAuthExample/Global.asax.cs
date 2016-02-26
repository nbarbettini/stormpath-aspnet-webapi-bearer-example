using System.Web.Http;

namespace BearerAuthExample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            StormpathConfig.Initialize();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
