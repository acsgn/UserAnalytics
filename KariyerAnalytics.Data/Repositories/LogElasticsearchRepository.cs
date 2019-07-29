using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogElasticsearchRepository : ILogElasticsearchRepository
    {
        private readonly static string _IndexName = "logs";
        public void Index(Log log)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                repository.Index(_IndexName, log);
            }
        }
        public void CreateIndex()
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                repository.CreateIndex(_IndexName);
            }
        }
    }
}
