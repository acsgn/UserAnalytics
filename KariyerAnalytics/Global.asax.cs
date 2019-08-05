using System.Web.Http;

namespace KariyerAnalytics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            DILoader.RegisterWebAPI(GlobalConfiguration.Configuration);
        }
    }
}
