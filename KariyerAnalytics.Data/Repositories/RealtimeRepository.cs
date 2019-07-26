using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class RealtimeRepository : IRealtimeRepository
    {
        public long GetRealtimeUserCount(int secondsBefore)
        {
            using (var repository = new ElasticsearchRepository<Log>())
            {
                var realtimeUsersRequest = new SearchDescriptor<Log>()
                    .Size(0)
                    .Aggregations(aggs => aggs
                        .Filter("filtered", fi => fi
                            .Filter(fil => fil
                                .DateRange(r => r
                                    .Field(f => f.Timestamp)
                                    .GreaterThanOrEquals(DateTime.Now.AddSeconds(-secondsBefore))
                                    .LessThanOrEquals(DateTime.Now)))));

                var realtimeUsersResult = repository.Search(realtimeUsersRequest);

                var count = realtimeUsersResult.Aggs.Filter("filtered").DocCount;

                return count;
            }
        }
        public RealtimeUserCountResponse[] GetRealtimeUserCountByEndpoints(int secondsBefore)
        {
            using (var repository = new ElasticsearchRepository<Log>())
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
                     select new RealtimeUserCountResponse
                     {
                         Endpoint = b.Key,
                         UserCount = (long)b.DocCount
                     }).ToArray();

                return realtimeUsersList;
            }
        }
    }
}
