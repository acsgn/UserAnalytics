using UserAnalytics.Business.Contract;
using UserAnalytics.Business.Entities;
using UserAnalytics.Data.Contract;
using System.Collections.Generic;

namespace UserAnalytics.Business
{
    public class LogElasticsearchEngine : ILogElasticsearchEngine
    {
        private ILogElasticsearchRepository _LogElasticsearchRepository;

        public LogElasticsearchEngine(ILogElasticsearchRepository repository)
        {
            _LogElasticsearchRepository = repository;
        }
        public void CreateIndex()
        {
            _LogElasticsearchRepository.CreateIndex();
        }

        public bool Add(Log log)
        {
            return _LogElasticsearchRepository.Index(log);
        }

        public bool AddMany(IEnumerable<Log> logs)
        {
            return _LogElasticsearchRepository.BulkIndex(logs);
        }
    }
}
