using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        public EndpointAbsoluteMetricsResponse GetBestResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", "endpoint")
                        .AddSubAggregation()
                            .AddContainer()
                                .AddAverageAggregation("average-response-time", "responseTime")
                                .Build()
                            .FinishSubAggregation()
                        .Build()
                    .AddContainer()
                        .AddMinBucketAggregation("best-response-time", "endpoints>average-response-time")
                        .Build()
                    .Build();

                var request = new SearchBuilder<Log>()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);

                var bucket = result.Aggs.MinBucket("best-response-time");

                return new EndpointAbsoluteMetricsResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    AverageResponseTime = (double)bucket.Value
                };
            }
        }

        public EndpointAbsoluteMetricsResponse GetWorstResponseTime(DateTime after, DateTime before)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", "endpoint")
                        .AddSubAggregation()
                            .AddContainer()
                                .AddAverageAggregation("average-response-time", "responseTime")
                                .Build()
                            .FinishSubAggregation()
                        .Build()
                    .AddContainer()
                        .AddMaxBucketAggregation("worst-response-time", "endpoints>average-response-time")
                        .Build()
                    .Build();

                var request = new SearchBuilder<Log>()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);

                var bucket = result.Aggs.MaxBucket("worst-response-time");

                return new EndpointAbsoluteMetricsResponse
                {
                    Endpoint = bucket.Keys.ToArray()[0],
                    AverageResponseTime = (double)bucket.Value
                };
            }
        }
        public EndpointMetricsResponse[] GetEndpointMetrics(DateTime after, DateTime before, string companyName, string username)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(username, "username")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", "endpoint")
                        .AddSubAggregation()
                            .AddContainer()
                                .AddMinAggregation("min-response-time", "responseTime")
                                .Build()
                            .AddContainer()
                                .AddAverageAggregation("average-response-time", "responseTime")
                                .Build()
                            .AddContainer()
                                .AddMaxAggregation("max-response-time", "responseTime")
                                .Build()
                            .FinishSubAggregation()
                        .Build()
                    .Build();

                var request = new SearchBuilder<Log>()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);

                var buckets = result.Aggs.Terms("endpoints").Buckets;

                var list = (from b in buckets
                                     select new EndpointMetricsResponse
                                     {
                                         Endpoint = b.Key,
                                         NumberOfRequests = (long)b.DocCount,
                                         MinResponseTime = (double)b.Min("min-response-time").Value,
                                         AverageResponseTime = (double)b.Average("average-response-time").Value,
                                         MaxResponseTime = (double)b.Max("max-response-time").Value,
                                     }).ToArray();

                return list;
            }
        }
    }
}
