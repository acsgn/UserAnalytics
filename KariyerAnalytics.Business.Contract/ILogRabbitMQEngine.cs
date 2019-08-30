using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogRabbitMQEngine
    {
        void Add(LogRequest logRequest);
        void Get(Func<Log, bool> func);
        void GetMany(Func<IEnumerable<Log>, bool> func);
        void CreateQueue();
        void StopConsumer();
        bool IsWorking();
    }
}