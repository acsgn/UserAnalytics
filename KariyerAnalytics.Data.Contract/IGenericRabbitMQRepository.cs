using System;

namespace KariyerAnalytics.Data.Contract
{
    public interface IGenericRabbitMQRepository<T> where T : class
    {
        void Queue(string routingKey, T obj);
        void Dequeue(string routingKey, Func<T, bool> target);
        void BulkDequeue(string routingKey, int bulk);
        void CreateQueue(string routingKey);
    }
}