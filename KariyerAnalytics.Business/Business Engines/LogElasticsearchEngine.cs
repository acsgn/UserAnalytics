using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Business
{
    public class LogElasticsearchEngine : ILogElasticsearchEngine
    {
        private readonly ILogElasticsearchRepository _Repository;

        public LogElasticsearchEngine(ILogElasticsearchRepository repository)
        {
            _Repository = repository;
        }

        public void CreateIndex()
        {
            _Repository.CreateIndex();
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

            _Repository.Index(log);
        }
    }
}
