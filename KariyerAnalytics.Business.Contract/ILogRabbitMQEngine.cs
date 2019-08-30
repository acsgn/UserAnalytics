using System;
using System.Collections.Generic;
using UserAnalytics.Business.Entities;
using UserAnalytics.Service.Entities;

namespace UserAnalytics.Business.Contract
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