using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Repositories;
using System.Collections.Generic;

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
                return repository.Index(log);
            }
        }

        public bool AddMany(IEnumerable<Log> logs)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                return repository.BulkIndex(logs);
            }
        }
    }
}
