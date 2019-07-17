using System;
using System.Web.Http;
using KariyerAnalytics.Business;

namespace KariyerAnalytics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            try
            {
                new LogEngine().Start();
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
