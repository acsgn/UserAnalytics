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

        private IGenericRabbitMQRepository<Log> _RabbitMQRepository;

        public LogRabbitMQRepository(IGenericRabbitMQRepository<Log> repository)
        {
            _RabbitMQRepository = repository;
        }
        public void Queue(Log log)
        {
            _RabbitMQRepository.Queue(_QueueName, log);
        }
        public void Dequeue(Func<Log, bool> func)
        {
            _RabbitMQRepository.Dequeue(_QueueName, func);
        }
        public void BulkDequeue(Func<IEnumerable<Log>, bool> func)
        {
            _RabbitMQRepository.BulkDequeue(_QueueName, _Bulk, func);
        }
        public void CreateQueue()
        {
            _RabbitMQRepository.CreateQueue(_QueueName);
        }
    }
}
