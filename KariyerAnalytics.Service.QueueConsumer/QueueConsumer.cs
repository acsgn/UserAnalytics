using KariyerAnalytics.Business;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Service.QueueConsumer
{
    public class QueueConsumer
    {
        public static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("I began");
            new LogRabbitMQEngine(new LogRabbitMQRepository()).GetMany(new LogElasticsearchEngine().AddMany);
        }
    }
}
