using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Data;

namespace KariyerAnalytics.Business
{
    public class LogEngine
    {
        private readonly static string IndexName = "logs";
        public void Start()
        {
            var context = new ElasticsearchContext();
            context.CreateIndex<Log>(IndexName);
        }
        public void Add(LogInformation info)
        {
            var log = new Log()
            {
                Company = info.CompanyName,
                User = info.Username,
                URL = info.URL,
                Date = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                //IP = info.IP,
                ResponseTime = info.ResponseTime
            };

            var rep = new EFRepository();
            rep.Add(IndexName, log);
        }
        public IEnumerable<KeyValuePair<string, double>> Search()
        {
            var rep = new EFRepository();
            return rep.Search<Log>();
        }
    }
}
