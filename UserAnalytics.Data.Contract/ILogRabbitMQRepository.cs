using System;
using System.Collections.Generic;
using UserAnalytics.Business.Entities;

namespace UserAnalytics.Data.Contract
{
    public interface ILogRabbitMQRepository
    {
        void CreateQueue();
        void Dequeue(Func<Log, bool> func);
        void BulkDequeue(Func<IEnumerable<Log>, bool> func);
        void Queue(Log log);
        bool CheckConnection();
    }
}