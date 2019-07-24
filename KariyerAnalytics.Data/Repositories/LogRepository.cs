using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly static string _IndexName = "logs";
        public void Index(Log log)
        {
            using (var repository = new GenericRepository<Log>())
            {
                repository.Index(_IndexName, log);
            }
        }
        public void CreateIndex()
        {
            using (var repository = new GenericRepository<Log>())
            {
                repository.CreateIndex(_IndexName);
            }
        }
    }
}
