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
                var query = repository.CreateQueryBuilder()
                    .AddDateRangeQuery(after, before, f => f.Timestamp)
                    .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                    .Build();

                var aggregation = repository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("companies", f => f.CompanyName, size, "average-response-time", ascending)
                        .AddSubAggregation()
                            .AddContainer()
                                .AddMinAggregation("min-response-time", f => f.ResponseTime)
                                .Build()
                            .AddContainer()
                                .AddAverageAggregation("average-response-time", f => f.ResponseTime)
                                .Build()
                            .AddContainer()
                                .AddMaxAggregation("max-response-time", f => f.ResponseTime)
                                .Build()
                            .FinishSubAggregation()
                        .Build()
                    .Build();

                var request = repository.CreateSearchBuilder()
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
                var query = repository.CreateQueryBuilder()
                    .AddDateRangeQuery(after, before, f => f.Timestamp)
                    .AddMatchPhraseQuery(companyName, f => f.CompanyName)
                    .AddMatchPhraseQuery(username, f => f.Username)
                    .Build();

                var aggregation = repository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", f => f.Endpoint, size, "average-response-time", ascending)
                        .AddSubAggregation()
                            .AddContainer()
                                .AddMinAggregation("min-response-time", f => f.ResponseTime)
                                .Build()
                            .AddContainer()
                                .AddAverageAggregation("average-response-time", f => f.ResponseTime)
                                .Build()
                            .AddContainer()
                                .AddMaxAggregation("max-response-time", f => f.ResponseTime)
                                .Build()
                            .FinishSubAggregation()
                        .Build()
                    .Build();

                var request = repository.CreateSearchBuilder()
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

        public MetricsResponse GetSingleMetric(string companyName, string username, string endpoint, DateTime? after, DateTime? before)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = repository.CreateQueryBuilder()
                    .AddDateRangeQuery(after, before, f => f.Timestamp)
                    .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                    .AddMatchPhraseQuery(companyName, f => f.CompanyName)
                    .AddMatchPhraseQuery(username, f => f.Username)
                    .Build();

                var aggregation = repository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddMinAggregation("min-response-time", f => f.ResponseTime)
                        .Build()
                    .AddContainer()
                        .AddAverageAggregation("average-response-time", f => f.ResponseTime)
                        .Build()
                    .AddContainer()
                        .AddMaxAggregation("max-response-time", f => f.ResponseTime)
                        .Build()
                    .Build();

                var request = repository.CreateSearchBuilder()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);

                return new MetricsResponse
                {
                    NumberOfRequests = result.HitsMetaData.Total,
                    MinResponseTime = (double)result.Aggs.Min("min-response-time").Value,
                    AverageResponseTime = (double)result.Aggs.Average("average-response-time").Value,
                    MaxResponseTime = (double)result.Aggs.Max("max-response-time").Value,
                };
            }
        }
    }
}
