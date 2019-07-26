using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogRabbitMQRepository : ILogRabbitMQRepository
    {
        private readonly static string _QueueName = "logs";
        public void Queue(Log log)
        {
            using (var repository = new RabbitMQRepository<Log>())
            {
                repository.Queue(_QueueName, log);
            }
        }
        public void Dequeue()
        {
            using (var repository = new RabbitMQRepository<Log>())
            {
                repository.Dequeue(_QueueName);
            }
        }
        public void CreateQueue()
        {
            using (var repository = new RabbitMQRepository<Log>())
            {
                repository.CreateQueue(_QueueName);
            }
        }
    }
}
