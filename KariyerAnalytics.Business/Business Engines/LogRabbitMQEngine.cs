using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;
using System;
using System.Collections.Generic;

namespace KariyerAnalytics.Business
{
    public class LogRabbitMQEngine : ILogRabbitMQEngine
    {
        private readonly ILogRabbitMQRepository _Repository;

        public LogRabbitMQEngine(ILogRabbitMQRepository repository)
        {
            _Repository = repository;
        }

        public void Add(LogRequest logRequest)
        {
            var log = new Log()
            {
                CompanyName = logRequest.CompanyName,
                Username = logRequest.Username,
                URL = logRequest.URL,
                Endpoint = logRequest.Endpoint,
                Timestamp = logRequest.Timestamp,
                IP = logRequest.IP,
                ResponseTime = logRequest.ResponseTime
            };

            _Repository.Queue(log);
        }

        public void Get(Func<Log, bool> func)
        {
            _Repository.Dequeue(func);
        }

        public void GetMany(Func<IEnumerable<Log>, bool> func)
        {
            _Repository.BulkDequeue(func);
        }

        public void CreateQueue()
        {
            _Repository.CreateQueue();
        }

        public void StopConsumer()
        {
            throw new NotImplementedException();
        }

        public bool IsWorking()
        {
            return _Repository.CheckConnection();
        }
    }
}
