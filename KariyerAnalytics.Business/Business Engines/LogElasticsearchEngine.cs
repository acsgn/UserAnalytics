using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace KariyerAnalytics.Business
{
    public class LogElasticsearchEngine : ILogElasticsearchEngine
    {        
        public void CreateIndex()
        {
            using (var repository = new LogElasticsearchRepository())
            {
                repository.CreateIndex();
            }
        }

        public bool Add(Log log)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                repository.Index(log);
                return true;
            }
        }

        public void AddMany(IEnumerable<LogRequest> logRequests)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var list = (from logRequest in logRequests
                            select
                            new Log()
                            {
                                CompanyName = logRequest.CompanyName,
                                Username = logRequest.Username,
                                URL = logRequest.URL,
                                Endpoint = logRequest.Endpoint,
                                Timestamp = logRequest.Timestamp,
                                IP = logRequest.IP,
                                ResponseTime = logRequest.ResponseTime
                            }).ToList();

                repository.BulkIndex(list);
            }
        }
    }
}
