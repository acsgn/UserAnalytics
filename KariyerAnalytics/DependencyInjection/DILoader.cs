using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Data.Contract;
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

            container.Register<ILogElasticsearchEngine, LogElasticsearchEngine>();
            container.Register<ILogRabbitMQEngine, LogRabbitMQEngine>();
            container.Register<IRealtimeEngine, RealtimeEngine>();
            container.Register<IHistogramEngine, HistogramEngine>();
            container.Register<IMetricEngine, MetricEngine>();
            container.Register<IInformationEngine, InformationEngine>();

            container.Register<ILogElasticsearchRepository, LogElasticsearchRepository>();
            container.Register<ILogRabbitMQRepository, LogRabbitMQRepository>();
            container.Register<IRealtimeRepository, RealtimeRepository>();
            container.Register<IHistogramRepository, HistogramRepository>();
            container.Register<IMetricRepository, MetricRepository>();
            container.Register<IInformationRepository, InformationRepository>();

            return container;
        }
        
    }
}