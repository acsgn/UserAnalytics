﻿using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class RealtimeRepository : IRealtimeRepository
    {
        public long GetRealtimeUserCount(int secondsBefore)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = repository.CreateQueryBuilder()
                    .AddDateRangeQuery(
                        DateTime.Now.AddSeconds(-secondsBefore),
                        DateTime.Now,
                        f => f.Timestamp)
                    .Build();
                
                var request = repository.CreateCountBuilder()
                    .AddQuery(query)
                    .Build();

                var result = repository.Count(request);

                var count = result.Count/secondsBefore;

                return count;
            }
        }
        public RealtimeUserCountResponse[] GetEndpointsRealtimeUserCount(int secondsBefore, int? size)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = repository.CreateQueryBuilder()
                    .AddDateRangeQuery(
                        DateTime.Now.AddSeconds(-secondsBefore),
                        DateTime.Now,
                        f => f.Timestamp)
                    .Build();

                var aggregation = repository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", f => f.Endpoint, size)
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
                             select new RealtimeUserCountResponse
                             {
                                 Endpoint = b.Key,
                                 UserCount = (long) b.DocCount/secondsBefore
                             }).ToArray();

                return list;
            }
        }
    }
}
