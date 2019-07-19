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
    public class LogEngine : IEngine
    {
        private readonly static string _IndexName = "logs";

        public void Start()
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

        public MetricResponse GetBestResponseTime(Request request)
        {
            var rep = new Repository();

            var bestRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f
                            .Endpoint)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS
                                .Field(f => f
                                    .ResponseTime))))
                    .MinBucket("best-response-time", s => s
                        .BucketsPath("actions>average-response-time")));

            
            var bestResult = rep.Search<Log>(bestRequest);
            var bucket = bestResult.Aggs.MaxBucket("best-response-time");

            return new MetricResponse
            {
                Endpoint = bucket.Keys.ToArray()[0],
                ResponseTime = (double) bucket.Value
            };
        }

        public MetricResponse GetWorstResponseTime(Request request)
        {
            var rep = new Repository();

            var worstRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f.Endpoint)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS
                                .Field(f => f
                                    .ResponseTime))))
                    .MaxBucket("worst-response-time", s => s
                        .BucketsPath("actions>average-response-time")));
            
            var request2 = new SearchRequest()
            {
                Query = new QueryBuilder().AddDateRangeFilter(request.After, request.Before, "timestamp").Build(),
                Aggregations = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("actions", "endpoint")
                        .AddSubAggregation()
                            .AddContainer()
                                .AddAverageAggregation("avg-res-time","responseTime")
                            .Build()
                        .FinishSubAggregation()
                    .Build()
                    .AddContainer()
                        .AddMaxBucketAggregation("worst-time", "actions>avg-res-time")
                    .Build()
                .Build()
            };

            var worstResult = rep.Search<Log>(request2);
            var bucket = worstResult.Aggs.MaxBucket("worst-time");

            return new MetricResponse
            {
                Endpoint = bucket.Keys.ToArray()[0],
                ResponseTime = (double) bucket.Value
            };
        }

        public string[] GetEndpoints(Request request)
        {
            var rep = new Repository();

            var endpointsRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("endpoints", s => s
                        .Field(f => f
                            .Endpoint)));
            var endpointsResult = rep.Search<Log>(endpointsRequest);

            var endpointList = (from b in endpointsResult.Aggs.Terms("endpoints").Buckets select b.Key).ToArray();

            return endpointList;
        }

        public int[] GetResponseTimes(string endpoint, Request request)
        {
            var rep = new Repository();

            var responseTimeRequest = new SearchDescriptor<Log>()
                .Query(q => q
                    .MatchPhrase(s => s
                        .Field(f => f.Endpoint)
                        .Query(endpoint)))
                .Sort(s => s
                    .Ascending(f => f
                        .Timestamp))
                .Fields(f => f
                    .Field(fi => fi
                        .ResponseTime));


            var responseTimeResult = rep.Search<Log>(responseTimeRequest);

            var responseTimeList = (from b in responseTimeResult.Fields select b.ValueOf<Log, int>(p => p.ResponseTime)).ToArray();

            return responseTimeList;
        }

        public string[] GetCompanies(Request request)
        {
            var rep = new Repository();

            var companiesRequest2 = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("companies", s => s
                        .Field(f => f
                            .CompanyName)));

            var companiesRequest = new SearchRequest
            {
                Size = 0,
                Query = new QueryBuilder().AddDateRangeFilter(request.After, request.Before, "timestamp").Build(),
                Aggregations = new AggregationBuilder()
                        .AddContainer()
                            .AddTermsAggregation("companies", "companyName")
                        .Build()
                    .Build()
            };

            var companiesResult = rep.Search<Log>(companiesRequest);

            var companyList = (from b in companiesResult.Aggs.Terms("companies").Buckets select b.Key).ToArray();

            return companyList;
        }

        public string[] GetCompanyDetails(DetailRequest detailRequest)
        {
            if (detailRequest.CompanyName != null && detailRequest.Username == null)
            {
                return GetUsersofCompany(detailRequest.CompanyName, detailRequest);
            }
            else if (detailRequest.CompanyName != null && detailRequest.Username != null)
            {
                return GetActionbyUserandCompany(detailRequest.CompanyName, detailRequest.Username, detailRequest);
            }
            else
            {
                return new string[0];
            }
        }

        private string[] GetUsersofCompany(string companyName, Request request)
        {
            var rep = new Repository();

            //var usersRequest = new SearchDescriptor<Log>()
            //    .Query(q => q
            //        .MatchPhrase(s => s
            //            .Field(f => f.CompanyName)
            //            .Query(companyName)))
            //    .Size(0)
            //    .Aggregations(aggs => aggs
            //        .Terms("users", s => s
            //            .Field(f => f.Username)));


            var usersRequest = new SearchRequest()
            {
                Query = new QueryBuilder()
                    .AddMatchQuery(companyName, "companyName")
                    .AddDateRangeFilter(request.After, request.Before, "timestamp")
                    .Build(),
                Size = 0,
                Aggregations = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("users", "username")
                        .Build()
                    .Build()
            };

            var usersResult = rep.Search<Log>(usersRequest);

            var userList = (from b in usersResult.Aggs.Terms("users").Buckets select b.Key).ToArray();

            return userList;
        }

        private string[] GetActionbyUserandCompany(string companyName, string username, Request request)
        {
            var rep = new Repository();

            var actionsRequest = new SearchDescriptor<Log>()
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f
                                    .CompanyName)
                                .Query(companyName)),
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f
                                    .Username)
                                .Query(username)))))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f
                            .Endpoint)));
            var actionsResult = rep.Search<Log>(actionsRequest);

            var actionsList = (from b in actionsResult.Aggs.Terms("actions").Buckets select b.Key).ToArray();

            return actionsList;
        }
        
    }
}
