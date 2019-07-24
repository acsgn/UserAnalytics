using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Business
{
    public class LogEngine : ILogEngine
    {
        private readonly ILogRepository _LogRepository;

        public LogEngine(ILogRepository logRepository)
        {
            _LogRepository = logRepository;
        }

        public void CreateIndex()
        {
            _LogRepository.CreateIndex();
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

            _LogRepository.Index(log);
        }
    }
}
