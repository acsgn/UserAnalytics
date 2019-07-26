using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        public EndpointAbsoluteMetricsResponse GetBestResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new ElasticsearchRepository<Log>())
            {
                var bestRequest = new SearchDescriptor<Log>()
                    .Size(0)
                    .Aggregations(aggs => aggs
                        .Filter("filtered", fi => fi
                            .Filter(fil => fil
                                .DateRange(r => r
                                    .Field(f => f.Timestamp)
                                    .GreaterThanOrEquals(after)
                                    .LessThanOrEquals(before)))
                            .Aggregations(nestedAggs => nestedAggs
                                .Terms("endpoints", s => s
                                    .Field(f => f.Endpoint)
                                    .Aggregations(nestedNestedAggs => nestedNestedAggs
                                        .Average("average-response-time", nestedS => nestedS
                                            .Field(f => f.ResponseTime))))
                                .MinBucket("best-response-time", s => s
                                    .BucketsPath("endpoints>average-response-time")))));
                
                var bestResult = repository.Search(bestRequest);
                var bucket = bestResult.Aggs.Filter("filtered").MinBucket("best-response-time");

                return new EndpointAbsoluteMetricsResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    AverageResponseTime = (double)bucket.Value
                };
            }
        }

        public EndpointAbsoluteMetricsResponse GetWorstResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new ElasticsearchRepository<Log>())
            {
                var worstRequest = new SearchDescriptor<Log>()
                    .Size(0)
                    .Aggregations(aggs => aggs
                        .Filter("filtered", fi => fi
                            .Filter(fil => fil
                                .DateRange(r => r
                                    .Field(f => f.Timestamp)
                                    .GreaterThanOrEquals(after)
                                    .LessThanOrEquals(before)))
                            .Aggregations(nestedAggs => nestedAggs
                                .Terms("endpoints", s => s
                                    .Field(f => f.Endpoint)
                                    .Aggregations(nestedNestedAggs => nestedNestedAggs
                                        .Average("average-response-time", nestedS => nestedS
                                            .Field(f => f.ResponseTime))))
                                .MaxBucket("worst-response-time", s => s
                                    .BucketsPath("endpoints>average-response-time")))));
                
                var worstResult = repository.Search(worstRequest);
                var bucket = worstResult.Aggs.Filter("filtered").MaxBucket("worst-response-time");

                return new EndpointAbsoluteMetricsResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    AverageResponseTime = (double)bucket.Value
                };
            }
        }
        public EndpointMetricsResponse[] GetEndpointMetrics(DateTime after, DateTime before, string companyName, string username)
        {
            using (var repository = new ElasticsearchRepository<Log>())
            {
                var endpointsRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Filter("filtered", fi => fi
                        .Filter(fil => fil
                            .DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))
                        .Aggregations(aggs2 => aggs2
                            .Filter("filtered2", fi2 => fi2
                                .Filter(fil => fil
                                    .MatchPhrase(s => s
                                        .Field(f => f.CompanyName)
                                        .Query(companyName)))
                                .Aggregations(aggs3 => aggs3
                                    .Filter("filtered3", fi3 => fi3
                                        .Filter(fil => fil
                                            .MatchPhrase(s => s
                                                .Field(f => f.Username)
                                                .Query(username)))
                                        .Aggregations(aggs4 => aggs4
                                            .Terms("endpoints", s => s
                                                .Field(f => f.Endpoint)
                                                .Aggregations(aggs5 => aggs5
                                                    .Min("min-response-time", a => a
                                                        .Field(f => f.ResponseTime))
                                                    .Average("average-response-time", a => a
                                                        .Field(f => f.ResponseTime))
                                                    .Max("max-response-time", a => a
                                                        .Field(f => f.ResponseTime)))))))))));

                var endpointsResult = repository.Search(endpointsRequest);

                var buckets = endpointsResult.Aggs.Filter("filtered").Filter("filtered2").Filter("filtered3").Terms("endpoints").Buckets;

                var endpointsList = (from b in buckets
                                     select new EndpointMetricsResponse
                                     {
                                         Endpoint = b.Key,
                                         NumberOfRequests = (long)b.DocCount,
                                         MinResponseTime = (double)b.Min("min-response-time").Value,
                                         AverageResponseTime = (double)b.Average("average-response-time").Value,
                                         MaxResponseTime = (double)b.Max("max-response-time").Value,
                                     }).ToArray();

                return endpointsList;
            }
        }
    }
}
