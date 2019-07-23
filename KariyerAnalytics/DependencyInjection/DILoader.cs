using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Data.Repositories;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace KariyerAnalytics.DependencyInjection
{
    public static class DILoader
    {
        public static Container Create()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<ILogEngine, LogEngine>();
            container.Register<ICompanyEngine, CompanyEngine>();
            container.Register<IStatisticEngine, StatisticEngine>();
            container.Register<LogRepository>();
            container.Register<StatisticRepository>();
            container.Register<CompanyRepository>();

            return container;
        }
        
    }
}