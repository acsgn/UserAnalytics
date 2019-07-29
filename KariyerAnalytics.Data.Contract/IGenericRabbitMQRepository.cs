namespace KariyerAnalytics.Data.Contract
{
    public interface IGenericRabbitMQRepository<T> where T : class
    {
        void Queue(string routingKey, T obj);
        void Dequeue(string routingKey);
        void CreateQueue(string routingKey);
    }
}