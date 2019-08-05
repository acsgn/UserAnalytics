using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class HistogramRepository : IHistogramRepository
    {
        private ILogElasticsearchRepository _LogElasticsearchRepository;

        public HistogramRepository(ILogElasticsearchRepository repository)
        {
            _LogElasticsearchRepository = repository;
        }

        public HistogramResponse[] GetResponseTimesHistogram(string endpoint, TimeSpan interval, DateTime? after, DateTime? before)
        {
            var query = LogElasticsearchRepository.CreateQueryBuilder()
                .AddDateRangeQuery(after, before, f => f.CompanyName)
                .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                .Build();

            var aggregation = LogElasticsearchRepository.CreateAggregationBuilder()
                .AddContainer()
                    .AddDateHistogram("histogram", f => f.Timestamp, interval)
                    .AddSubAggregation()
                        .AddContainer()
                            .AddAverageAggregation("average-response-time", f => f.ResponseTime)
                            .Build()
                        .FinishSubAggregation()
                    .Build()
                .Build();

            var request = LogElasticsearchRepository.CreateSearchBuilder()
                .SetSize(0)
                .AddQuery(query)
                .AddAggregation(aggregation)
                .Build();

            var result = _LogElasticsearchRepository.Search(request);

            var buckets = result.Aggs.DateHistogram("histogram").Buckets;

            var list = (from b in buckets
                        select new HistogramResponse
                        {
                            Timestamp = b.Date,
                            NumberOfRequests = b.DocCount,
                            Average = (double)b.Average("average-response-time").Value
                        }).ToArray();

            return list;
        }
    }
}
