using System.Web.Http;
using KariyerAnalytics.DependencyInjection;

namespace KariyerAnalytics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MefConfig.RegisterMef();
        }
    }
}
