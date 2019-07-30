using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        public MetricsResponse[] GetCompanyMetrics(string endpoint, int? size, bool? ascending, DateTime? after, DateTime? before)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("companies", "companyName", size, "average-response-time", ascending)
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

                var buckets = result.Aggs.Terms("companies").Buckets;

                var list = (from b in buckets
                            select new MetricsResponse
                            {
                                Key = b.Key,
                                NumberOfRequests = (long)b.DocCount,
                                MinResponseTime = (double)b.Min("min-response-time").Value,
                                AverageResponseTime = (double)b.Average("average-response-time").Value,
                                MaxResponseTime = (double)b.Max("max-response-time").Value,
                            }).ToArray();

                return list;
            }
        }

        public MetricsResponse[] GetEndpointMetrics(string companyName, string username, int? size, bool? ascending, DateTime? after, DateTime? before)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(username, "username")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", "endpoint", size, "average-response-time", ascending)
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
                            select new MetricsResponse
                            {
                                Key = b.Key,
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
