using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;
using System.ComponentModel.Composition;
using KariyerAnalytics.Common.DependencyInjection;

namespace KariyerAnalytics.Business
{
    [Export(typeof(ILogEngine))]
    public class LogEngine : ILogEngine
    {
        [Import]
        private LogRepository _LogRepository;

        public LogEngine()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
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
