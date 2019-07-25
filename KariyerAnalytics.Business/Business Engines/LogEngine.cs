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

            _LogRepository.Index(log);
        }
    }
}
