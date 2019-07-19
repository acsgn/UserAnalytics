using System.Collections.Generic;
using System.Linq;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data;
using KariyerAnalytics.Data.Repositories;
using Nest;

namespace KariyerAnalytics.Business
{
    public class LogEngine : ILogEngine
    {
        private readonly static string _IndexName = "logs";

        public void CreateIndex()
        {
            var rep = new Repository();
            rep.CreateIndex<Log>(_IndexName);
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

            var rep = new Repository();
            rep.Index(_IndexName, log);
        }
    }
}
