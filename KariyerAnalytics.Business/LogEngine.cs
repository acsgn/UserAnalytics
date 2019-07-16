using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Data;

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

        public void GetBestAndWorstResponseTime()
        {
            var rep = new EFRepository();

            var bestRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f.URL)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS.Field(f => f.ResponseTime))))
                    .MinBucket("best-response-time", s => s.BucketsPath("actions>average-response-time")));
            var bestResult = rep.Search<Log>(bestRequest);

            var worstRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f.URL)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS.Field(f => f.ResponseTime))))
                    .MaxBucket("worst-response-time", s => s.BucketsPath("actions>average-response-time")));
            var worstResult = rep.Search<Log>(worstRequest);

            
        }

        public void GetCompanies()
        {
            var rep = new EFRepository();

            var companiesRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("companies", s => s
                        .Field(f => f.CompanyName)));
            var companiesResult = rep.Search<Log>(companiesRequest);
        }

        public void GetUsersofCompany(string companyName)
        {
            var rep = new EFRepository();

            var usersRequest = new Nest.SearchDescriptor<Log>()
                .Query(q => q
                    .MatchPhrase(s => s
                        .Field(f => f.CompanyName)
                        .Query(companyName)))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("users", s => s
                        .Field(f => f.Username)));
            rep.Search<Log>(usersRequest);
        }

        public void GetActionbyUserandCompany(string companyName, string username)
        {
            var rep = new EFRepository();

            var actionsRequest = new Nest.SearchDescriptor<Log>()
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f.CompanyName)
                                .Query(companyName)),
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f.Username)
                                .Query(username)))))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f.URL)));
            rep.Search<Log>(actionsRequest);
        }

    }
}
