using System.ServiceProcess;
using KariyerAnalytics.Common.DependencyInjection;

namespace KariyerAnalytics.Service.QueueConsumer
{
    public partial class QueueConsumer : ServiceBase
    {
        public QueueConsumer()
        {
            InitializeLifetimeService();
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            var rabbitMQEngine = DILoader.ResolveLogRabbitMQEngine();
            var elasticsearchEngine = DILoader.ResolveLogElasticsearchEngine();
            rabbitMQEngine.GetMany(elasticsearchEngine.AddMany);
        }

        protected override void OnStop()
        {

        }
    }
}
