using System.Web.Http;
using KariyerAnalytics.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace KariyerAnalytics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = DILoader.Create();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
