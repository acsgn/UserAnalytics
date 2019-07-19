using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Business
{
    public class LogEngine : ILogEngine
    {

        public void CreateIndex()
        {
            var rep = new LogRepository();
            rep.CreateIndex();
        }

        public void Add(LogInformation info)
        {
            var log = new Log()
            {
                CompanyName = info.CompanyName,
                Username = info.Username,
                URL = info.URL,
                Endpoint = info.Endpoint,
                Timestamp = info.Timestamp,
                IP = info.IP,
                ResponseTime = info.ResponseTime
            };

            var rep = new LogRepository();
            rep.Index(log);
        }
    }
}
