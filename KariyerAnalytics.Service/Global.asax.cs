using System.Web.Http;
using UserAnalytics.Common.DependencyInjection;
using UserAnalytics.Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace UserAnalytics
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
