using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface ILogRabbitMQRepository
    {
        void CreateQueue();
        void Dequeue();
        void Queue(Log log);
    }
}