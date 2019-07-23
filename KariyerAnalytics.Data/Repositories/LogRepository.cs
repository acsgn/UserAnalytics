using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogRepository
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
