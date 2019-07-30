using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        public string[] GetEndpoints(string companyName, string username, DateTime after, DateTime before)
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
                        .AddTermsAggregation("endpoints", "endpoint")
                        .Build()
                    .Build();

                var request = new SearchBuilder<Log>()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);
                
                var buckets = result.Aggs.Terms("endpoints").Buckets;

                var list = (from b in buckets select b.Key).ToArray();

                return list;
            }
        }
        public string[] GetCompanies(string endpoint, string username, DateTime after, DateTime before)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .AddMatchPhraseQuery(username, "username")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("companies", "companyName")
                        .Build()
                    .Build();

                var request = new SearchBuilder<Log>()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);

                var buckets = result.Aggs.Terms("companies").Buckets;

                var list = (from b in buckets select b.Key).ToArray();

                return list;
            }
        }
        public string[] GetUsers(string endpoint, string companyName, DateTime after, DateTime before)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddDateRangeQuery(after, before, "timestamp")
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("users", "username")
                        .Build()
                    .Build();

                var request = new SearchBuilder<Log>()
                    .SetSize(0)
                    .AddQuery(query)
                    .AddAggregation(aggregation)
                    .Build();

                var result = repository.Search(request);

                var buckets = result.Aggs.Terms("users").Buckets;

                var list = (from b in buckets select b.Key).ToArray();

                return list;
            }
        }
    }
}
