using System;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface ILogRabbitMQRepository
    {
        void CreateQueue();
        void Dequeue(Func<Log, bool> target);
        void BulkDequeue();
        void Queue(Log log);
    }
}