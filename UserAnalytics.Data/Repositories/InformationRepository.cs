using System.Linq;
using UserAnalytics.Data.Contract;

namespace UserAnalytics.Data.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private ILogElasticsearchRepository _LogElasticsearchRepository;

        public InformationRepository(ILogElasticsearchRepository repository)
        {
            _LogElasticsearchRepository = repository;
        }

        public string[] GetEndpoints(string endpoint, string companyName, string username, int? size)
        {
            var query = LogElasticsearchRepository.CreateQueryBuilder()
                .AddMatchPhraseQuery(companyName, f => f.CompanyName)
                .AddMatchPhraseQuery(username, f => f.Username)
                .AddPrefixMatchQuery(endpoint, f => f.Endpoint)
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

            var list = (from b in buckets select b.Key).ToArray();

            return list;
        }
        public string[] GetCompanies(string endpoint, string companyName, string username, int? size)
        {
            var query = LogElasticsearchRepository.CreateQueryBuilder()
                .AddPrefixMatchQuery(companyName, f => f.CompanyName)
                .AddMatchPhraseQuery(username, f => f.Username)
                .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                .Build();

            var aggregation = LogElasticsearchRepository.CreateAggregationBuilder()
                .AddContainer()
                    .AddTermsAggregation("companies", f => f.CompanyName, size)
                    .Build()
                .Build();

            var request = LogElasticsearchRepository.CreateSearchBuilder()
                .SetSize(0)
                .AddQuery(query)
                .AddAggregation(aggregation)
                .Build();

            var result = _LogElasticsearchRepository.Search(request);

            var buckets = result.Aggs.Terms("companies").Buckets;

            var list = (from b in buckets select b.Key).ToArray();

            return list;

        }
        public string[] GetUsers(string endpoint, string companyName, string username, int? size)
        {
            var query = LogElasticsearchRepository.CreateQueryBuilder()
                .AddMatchPhraseQuery(companyName, f => f.CompanyName)
                .AddPrefixMatchQuery(username, f => f.Username)
                .AddMatchPhraseQuery(endpoint, f => f.Endpoint)
                .Build();

            var aggregation = LogElasticsearchRepository.CreateAggregationBuilder()
                .AddContainer()
                    .AddTermsAggregation("users", f => f.Username, size)
                    .Build()
                .Build();

            var request = LogElasticsearchRepository.CreateSearchBuilder()
                .SetSize(0)
                .AddQuery(query)
                .AddAggregation(aggregation)
                .Build();

            var result = _LogElasticsearchRepository.Search(request);

            var buckets = result.Aggs.Terms("users").Buckets;

            var list = (from b in buckets select b.Key).ToArray();

            return list;
        }
    }
}
