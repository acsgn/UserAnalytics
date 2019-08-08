using System.ServiceProcess;
using KariyerAnalytics.Business.Entities;
//using KariyerAnalytics.Common.DependencyInjection;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Service.QueueConsumer
{
    public partial class QueueConsumer : ServiceBase
    {
        public QueueConsumer()
        {
            InitializeLifetimeService();
        }

        protected override void OnStart(string[] args)
        {
            var rabbitMQEngine = new LogRabbitMQRepository(new GenericRabbitMQRepository<Log>());
            var elasticsearchEngine = new LogElasticsearchRepository(new GenericElasticsearchRepository<Log>());
            rabbitMQEngine.BulkDequeue(elasticsearchEngine.BulkIndex);
        }

        protected override void OnStop()
        {

        }
    }
}
