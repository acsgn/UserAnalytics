using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        public MetricResponse GetBestResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
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

                return new MetricResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    AverageResponseTime = (double)bucket.Value
                };
            }
           
        }

        public MetricResponse GetWorstResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
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

                return new MetricResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    AverageResponseTime = (double)bucket.Value
                };
            }
        }

        public RealtimeUserMetric[] GetRealtimeUsers(int secondsBefore)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var realtimeUsersRequest = new SearchDescriptor<Log>()
                    .Size(0)
                    .Aggregations(aggs => aggs
                        .Filter("filtered", fi => fi
                            .Filter(fil => fil
                                .DateRange(r => r
                                    .Field(f => f.Timestamp)
                                    .GreaterThanOrEquals(DateTime.Now.AddSeconds(-secondsBefore))
                                    .LessThanOrEquals(DateTime.Now)))
                            .Aggregations(nestedAggs => nestedAggs
                                .Terms("endpoints", t => t
                                    .Field(f => f.Endpoint)))));

                var realtimeUsersResult = repository.Search(realtimeUsersRequest);

                var buckets = realtimeUsersResult.Aggs.Filter("filtered").Terms("endpoints").Buckets;

                var realtimeUsersList =
                    (from b in buckets
                    select new RealtimeUserMetric
                    {
                        Endpoint = b.Key,
                        UserCount = (long)b.DocCount
                    }).ToArray();

                return realtimeUsersList;
            }
        }

        public string[] GetEndpoints(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
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
                            .Aggregations(nestedAggs => nestedAggs
                                .Terms("endpoints", s => s
                                    .Field(f => f.Endpoint)))));

                var endpointsResult = repository.Search(endpointsRequest);

                var endpointList = (from b in endpointsResult.Aggs.Filter("filtered").Terms("endpoints").Buckets select b.Key).ToArray();

                return endpointList;
            }
        }

        public Histogram[] GetResponseTimes(TimeSpan interval, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var responseTimeRequest = new SearchDescriptor<Log>()
                    .Size(0)
                    .Aggregations(aggs => aggs
                        .Filter("filtered", fi => fi
                            .Filter(fil => fil
                                .DateRange(r => r
                                    .Field(f => f.Timestamp)
                                    .GreaterThanOrEquals(after)
                                    .LessThanOrEquals(before)))
                            .Aggregations(nestedAggs => nestedAggs
                                    .DateHistogram("histogram", dh => dh
                                        .Field(f => f.Timestamp)
                                        .Interval(interval)
                                        .Aggregations(nestedNestedAggs => nestedNestedAggs
                                            .Average("average-response-time", a => a.Field(f => f.ResponseTime))
                                        )
                                    )
                                )
                            )
                        );


                var responseTimeResult = repository.Search(responseTimeRequest);

                var buckets = responseTimeResult.Aggs.Filter("filtered").DateHistogram("histogram").Buckets;

                var responseTimeList = (from b in buckets
                                        select new Histogram
                                        {
                                            Timestamp = b.Date,
                                            NumberOfRequests = (long)b.DocCount,
                                            Average = (double)b.Average("average-response-time").Value
                                        }).ToArray();

                return responseTimeList;
            }
        }

        public Histogram[] GetResponseTimesByEndpoint(string endpoint, TimeSpan interval, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var responseTimeRequest = new SearchDescriptor<Log>()
                    .Size(0)
                    .Aggregations(aggs => aggs
                        .Filter("filtered", fi => fi
                            .Filter(fil => fil
                                .DateRange(r => r
                                    .Field(f => f.Timestamp)
                                    .GreaterThanOrEquals(after)
                                    .LessThanOrEquals(before)))
                            .Aggregations(nestedAggs => nestedAggs
                                .Filter("filtered2", fi2 => fi2
                                        .Filter(fil => fil
                                            .MatchPhrase(s => s
                                                .Field(f => f.Endpoint)
                                                .Query(endpoint)))
                                        .Aggregations(nestedNestedNestedAggs => nestedNestedNestedAggs
                                            .DateHistogram("histogram", dh => dh
                                                .Field(f => f.Timestamp)
                                                .Interval(interval)
                                                .Aggregations(nestedNestedAggs => nestedNestedAggs
                                                    .Average("average-response-time", a => a
                                                        .Field(f => f.ResponseTime)))))))));
                                            
                
                var responseTimeResult = repository.Search(responseTimeRequest);

                var buckets = responseTimeResult.Aggs.Filter("filtered").Filter("filtered2").DateHistogram("histogram").Buckets;

                var responseTimeList = (from b in buckets
                                        select new Histogram
                                        {
                                            Timestamp = b.Date,
                                            NumberOfRequests = (long) b.DocCount,
                                            Average = (double) b.Average("average-response-time").Value
                                        }).ToArray();

                return responseTimeList;
            }

        }

    }
}
