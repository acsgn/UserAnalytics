using System.Web.Http;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Data.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace KariyerAnalytics
{
    public static class DILoader
    {
        public static Container Container { get; private set; }

        static DILoader()
        {
            Container = new Container();

            Container.Options.DefaultLifestyle = Lifestyle.Singleton;

            Container.Register<ILogElasticsearchEngine, LogElasticsearchEngine>();
            Container.Register<ILogRabbitMQEngine, LogRabbitMQEngine>();
            Container.Register<IRealtimeEngine, RealtimeEngine>();
            Container.Register<IHistogramEngine, HistogramEngine>();
            Container.Register<IMetricEngine, MetricEngine>();
            Container.Register<IInformationEngine, InformationEngine>();

            Container.Register<ILogElasticsearchRepository, LogElasticsearchRepository>();
            Container.Register<ILogRabbitMQRepository, LogRabbitMQRepository>();
            Container.Register<IRealtimeRepository, RealtimeRepository>();
            Container.Register<IHistogramRepository, HistogramRepository>();
            Container.Register<IMetricRepository, MetricRepository>();
            Container.Register<IInformationRepository, InformationRepository>();

            Container.Register<IGenericElasticsearchRepository<Log>, GenericElasticsearchRepository<Log>>();
            Container.Register<IGenericRabbitMQRepository<Log>, GenericRabbitMQRepository<Log>>();
        }
        
        public static void RegisterWebAPI(HttpConfiguration configuration)
        {
            Container.RegisterWebApiControllers(configuration);
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Container);
        }

        public static ILogElasticsearchEngine ResolveLogElasticsearchEngine()
        {
            return Container.GetInstance<ILogElasticsearchEngine>();
        }
        public static ILogRabbitMQEngine ResolveLogRabbitMQEngine()
        {
            return Container.GetInstance<ILogRabbitMQEngine>();
        }
    }
}