using System;
using System.Linq;
using UserAnalytics.Business.Entities;
using UserAnalytics.Data.Contract;

namespace UserAnalytics.Data.Repositories
{
    public class RealtimeRepository : IRealtimeRepository
    {
        private ILogElasticsearchRepository _LogElasticsearchRepository;

        public RealtimeRepository(ILogElasticsearchRepository repository)
        {
            _LogElasticsearchRepository = repository;
        }

        public long GetRealtimeUserCount(int secondsBefore)
        {
                var query = LogElasticsearchRepository.CreateQueryBuilder()
                    .AddDateRangeQuery(
                        DateTime.Now.AddSeconds(-secondsBefore),
                        DateTime.Now,
                        f => f.Timestamp)
                    .Build();
                
                var request = LogElasticsearchRepository.CreateCountBuilder()
                    .AddQuery(query)
                    .Build();

                var result = _LogElasticsearchRepository.Count(request);

                var count = result.Count/secondsBefore;

                return count;
        }
        public RealtimeUserCountResponse[] GetEndpointsRealtimeUserCount(int secondsBefore, int? size)
        {
                var query = LogElasticsearchRepository.CreateQueryBuilder()
                    .AddDateRangeQuery(
                        DateTime.Now.AddSeconds(-secondsBefore),
                        DateTime.Now,
                        f => f.Timestamp)
                    .Build();

                var aggregation = LogElasticsearchRepository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", f => f.Endpoint, size)
                        .Build()
                    .Build();

                var request = LogElasticsearchRepository.CreateSearchBuilder()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = _LogElasticsearchRepository.Search(request);

                var buckets = result.Aggs.Terms("endpoints").Buckets;

                var list = (from b in buckets
                             select new RealtimeUserCountResponse
                             {
                                 Endpoint = b.Key,
                                 UserCount = (long) b.DocCount/secondsBefore
                             }).ToArray();

                return list;
        }
    }
}
