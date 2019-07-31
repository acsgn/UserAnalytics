using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class HistogramRepository : IHistogramRepository
    {
        public HistogramResponse[] GetResponseTimesHistogram(string endpoint, TimeSpan interval, DateTime? after, DateTime? before)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddDateHistogram("histogram", "timestamp", interval)
                        .AddSubAggregation()
                            .AddContainer()
                                .AddAverageAggregation("average-response-time", "responseTime")
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

                var buckets = result.Aggs.DateHistogram("histogram").Buckets;

                var  list = (from b in buckets
                             select new HistogramResponse
                             {
                                Timestamp = b.Date,
                                NumberOfRequests = b.DocCount,
                                Average = (double) b.Average("average-response-time").Value
                             }).ToArray();

                return list;
            }

        }
    }
}
