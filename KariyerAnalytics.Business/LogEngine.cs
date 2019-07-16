using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Data;
using KariyerAnalytics.Data.Entities;

namespace KariyerAnalytics.Business
{
    public class LogEngine
    {
        private readonly static string _IndexName = "logs";
        public void Start()
        {
            var context = new ElasticsearchContext();
            context.CreateIndex<Log>(_IndexName);
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

            var rep = new EFRepository();
            rep.Add(_IndexName, log);
        }

        public MinMaxResponse GetBestAndWorstTime()
        {
            var rep = new EFRepository();
            var maxrequest = new Nest.SearchDescriptor<Log>().Size(1).Aggregations(agg => agg
                .Max("max", e2 => e2.Field("responsetime"))
                );
            var maxresult = rep.Search<Log>(maxrequest).Max("max").Value;
            var minrequest = new Nest.SearchDescriptor<Log>().Size(0).Aggregations(agg => agg
                .Min("min", e2 => e2.Field("responsetime"))
                );
            var minresult = rep.Search<Log>(minrequest).Min("min");

            return new MinMaxResponse
            {
            };
        }

    }
}
