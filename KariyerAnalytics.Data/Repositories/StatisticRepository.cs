using System;
using System.ComponentModel.Composition;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    [Export(typeof(StatisticRepository))]
    public class StatisticRepository
    {
        public MetricResponse GetBestResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var bestRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Query(q => q
                    .DateRange(r => r
                        .Field(f => f.Timestamp)
                        .GreaterThanOrEquals(after)
                        .LessThanOrEquals(before)))
                .Aggregations(aggs => aggs
                    .Terms("endpoints", s => s
                        .Field(f => f.Endpoint)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS
                                .Field(f => f.ResponseTime))))
                    .MinBucket("best-response-time", s => s
                        .BucketsPath("endpoints>average-response-time")));
                
                var bestResult = repository.Search(bestRequest);
                var bucket = bestResult.Aggs.MaxBucket("best-response-time");

                return new MetricResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    ResponseTime = (double)bucket.Value
                };
            }
           
        }

        public MetricResponse GetWorstResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var worstRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Query(q => q
                    .DateRange(r => r
                        .Field(f => f.Timestamp)
                        .GreaterThanOrEquals(after)
                        .LessThanOrEquals(before)))
                .Aggregations(aggs => aggs
                    .Terms("endpoints", s => s
                        .Field(f => f.Endpoint)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS
                                .Field(f => f.ResponseTime))))
                    .MaxBucket("worst-response-time", s => s
                        .BucketsPath("endpoints>average-response-time")));
                
                var worstResult = repository.Search(worstRequest);
                var bucket = worstResult.Aggs.MaxBucket("worst-response-time");

                return new MetricResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    ResponseTime = (double)bucket.Value
                };
            }
        }

        public long GetRealtimeUsers(int secondsBefore)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var realtimeUsersRequest = new CountDescriptor<Log>()
                    .Query(q => q
                        .DateRange(dr => dr
                            .GreaterThanOrEquals(DateTime.Now.AddSeconds(secondsBefore))
                            .LessThanOrEquals(DateTime.Now)));

                var realtimeUsersResult = repository.Count(realtimeUsersRequest);
                
                return realtimeUsersResult.Count;
            }
        }

        public string[] GetEndpoints(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var endpointsRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Query(q => q
                    .DateRange(r => r
                        .Field(f => f.Timestamp)
                        .GreaterThanOrEquals(after)
                        .LessThanOrEquals(before)))
                .Aggregations(aggs => aggs
                    .Terms("endpoints", s => s
                        .Field(f => f.Endpoint)));

                var endpointsResult = repository.Search(endpointsRequest);

                var endpointList = (from b in endpointsResult.Aggs.Terms("endpoints").Buckets select b.Key).ToArray();

                return endpointList;
            }
        }

        public int[] GetResponseTimes(string endpoint, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var responseTimeRequest = new SearchDescriptor<Log>()
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            mu => mu
                            .MatchPhrase(mp => mp
                                .Field(f => f.Endpoint)
                                .Query(endpoint)),
                            mu => mu
                            .DateRange(dr => dr
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))))
                .Sort(s => s
                    .Ascending(f => f.Timestamp))
                .Fields(f => f
                    .Field(fi => fi.ResponseTime));
                
                var responseTimeResult = repository.Search(responseTimeRequest);

                var responseTimeList = (from b in responseTimeResult.Fields select b.ValueOf<Log, int>(p => p.ResponseTime)).ToArray();

                return responseTimeList;
            }

        }

    }
}
