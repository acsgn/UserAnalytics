using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogRabbitMQEngine
    {
        void Add(LogRequest logRequest);
        void Get();
        void CreateQueue();
    }
}