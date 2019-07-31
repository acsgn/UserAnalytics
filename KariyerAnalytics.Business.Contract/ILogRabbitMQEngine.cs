using System;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogRabbitMQEngine
    {
        void Add(LogRequest logRequest);
        void Get(Func<Log, bool> target);
        void CreateQueue();
    }
}