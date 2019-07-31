using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface ILogRabbitMQRepository
    {
        void CreateQueue();
        void Dequeue(Func<Log, bool> func);
        void BulkDequeue(Func<IEnumerable<Log>, bool> func);
        void Queue(Log log);
    }
}