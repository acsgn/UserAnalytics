using System.Web.Http;
using KariyerAnalytics.Common.DependencyInjection;
using KariyerAnalytics.Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace KariyerAnalytics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DILoader.Container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(DILoader.Container);
        }
    }
}
