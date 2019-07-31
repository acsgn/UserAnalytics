using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogRabbitMQRepository : ILogRabbitMQRepository
    {
        private readonly static string _QueueName = "logs";
        private readonly static int _Bulk = 100;
        public void Queue(Log log)
        {
            using (var repository = new GenericRabbitMQRepository<Log>())
            {
                repository.Queue(_QueueName, log);
            }
        }
        public void Dequeue(Func<Log, bool> func)
        {
            using (var repository = new GenericRabbitMQRepository<Log>())
            {
                repository.Dequeue(_QueueName, func);
            }
        }
        public void BulkDequeue(Func<IEnumerable<Log>, bool> func)
        {
            using (var repository = new GenericRabbitMQRepository<Log>())
            {
                repository.BulkDequeue(_QueueName, _Bulk, func);
            }
        }
        public void CreateQueue()
        {
            using (var repository = new GenericRabbitMQRepository<Log>())
            {
                repository.CreateQueue(_QueueName);
            }
        }
    }
}
