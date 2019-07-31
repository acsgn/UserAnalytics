using System.Linq;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        public string[] GetEndpoints(string endpoint, string companyName, string username, int? size)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(username, "username")
                    .AddPrefixMatchQuery(endpoint, "endpoint")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("endpoints", "endpoint", size)
                        .Build()
                    .Build();
                
                var request = repository.CreateSearchBuilder()
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
        public string[] GetCompanies(string endpoint, string companyName, string username, int? size)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddPrefixMatchQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(username, "username")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("companies", "companyName", size)
                        .Build()
                    .Build();

                var request = repository.CreateSearchBuilder()
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
        public string[] GetUsers(string endpoint, string companyName, string username, int? size)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddPrefixMatchQuery(username, "username")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var aggregation = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("users", "username", size)
                        .Build()
                    .Build();

                var request = repository.CreateSearchBuilder()
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
