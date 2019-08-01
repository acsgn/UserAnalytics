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
                var query = repository.CreateQueryBuilder()
                    .AddMatchPhraseQuery(companyName, f => f.CompanyName)
                    .AddMatchPhraseQuery(username, f => f.Username)
                    .AddPrefixMatchQuery(endpoint, f => f.Endpoint)
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

                var list = (from b in buckets select b.Key).ToArray();

                return list;
            }
        }
        public string[] GetCompanies(string endpoint, string companyName, string username, int? size)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = repository.CreateQueryBuilder()
                    .AddPrefixMatchQuery(companyName, f => f.CompanyName)
                    .AddMatchPhraseQuery(username, f => f.Username)
                    .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                    .Build();

                var aggregation = repository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("companies", f => f.CompanyName, size)
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
                var query = repository.CreateQueryBuilder()
                    .AddMatchPhraseQuery(companyName, f => f.CompanyName)
                    .AddPrefixMatchQuery(username, f => f.Username)
                    .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                    .Build();

                var aggregation = repository.CreateAggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("users", f => f.Username, size)
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
