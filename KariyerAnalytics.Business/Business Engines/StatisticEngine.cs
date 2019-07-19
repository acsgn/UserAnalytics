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
    public class StatisticEngine : IStatisticEngine
    {
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
                ResponseTime = (double)bucket.Value
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
                                .AddAverageAggregation("avg-res-time", "responseTime")
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
                ResponseTime = (double)bucket.Value
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



    }
}
