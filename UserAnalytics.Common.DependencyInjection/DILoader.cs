using UserAnalytics.Business;
using UserAnalytics.Business.Contract;
using UserAnalytics.Business.Entities;
using UserAnalytics.Data.Contract;
using UserAnalytics.Data.Repositories;
using SimpleInjector;

namespace UserAnalytics.Common.DependencyInjection
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