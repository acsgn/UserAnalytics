using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;
using System;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Business
{
    public class LogRabbitMQEngine : ILogRabbitMQEngine
    {
        private readonly ILogRabbitMQRepository _Repository;

        public LogRabbitMQEngine()
        {
            _Repository = new LogRabbitMQRepository();
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

        public void Get(Func<Log, bool> target)
        {
            _Repository.Dequeue(target);
        }

        public void GetMany()
        {
            _Repository.BulkDequeue();
        }

        public void CreateQueue()
        {
            _Repository.CreateQueue();
        }
    }
}
