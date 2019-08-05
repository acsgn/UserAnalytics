using System.ServiceProcess;
using KariyerAnalytics.Business;
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
            new LogRabbitMQEngine(new LogRabbitMQRepository()).GetMany(new LogElasticsearchEngine().AddMany);
        }

        protected override void OnStop()
        {
        }
    }
}
