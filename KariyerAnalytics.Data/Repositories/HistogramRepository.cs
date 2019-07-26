using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class HistogramRepository : IHistogramRepository
    {
        public HistogramResponse[] GetResponseTimesHistogram(TimeSpan interval, DateTime after, DateTime before, string endpoint)
        {
            using (var repository = new ElasticsearchRepository<Log>())
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
                                        select new HistogramResponse
                                        {
                                            Timestamp = b.Date,
                                            NumberOfRequests = b.DocCount,
                                            Average = (double) b.Average("average-response-time").Value
                                        }).ToArray();

                return responseTimeList;
            }

        }
    }
}
