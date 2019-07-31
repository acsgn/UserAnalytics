using System;
using System.Collections.Generic;

namespace KariyerAnalytics.Data.Contract
{
    public interface IGenericRabbitMQRepository<T> where T : class
    {
        void Queue(string routingKey, T obj);
        void Dequeue(string routingKey, Func<T, bool> func);
        void BulkDequeue(string routingKey, int bulk, Func<IEnumerable<T>, bool> func);
        void CreateQueue(string routingKey);
    }
}