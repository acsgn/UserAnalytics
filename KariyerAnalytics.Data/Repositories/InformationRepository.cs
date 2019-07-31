using System.Linq;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Data.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        public string[] GetEndpoints(string endpoint, string companyName, string username)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(username, "username")
                    .AddPrefixMatchQuery(endpoint, "endpoint")
                    .Build();
                
                var request = repository.CreateSearchBuilder()
                    .AddQuery(query)
                    .Build();

                var result = repository.Search(request);

                var buckets = result.Documents;

                var list = (from b in buckets select b.Endpoint).ToArray();

                return list;
            }
        }
        public string[] GetCompanies(string endpoint, string companyName, string username)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddPrefixMatchQuery(companyName, "companyName")
                    .AddMatchPhraseQuery(username, "username")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var request = repository.CreateSearchBuilder()
                    .AddQuery(query)
                    .Build();

                var result = repository.Search(request);

                var buckets = result.Documents;

                var list = (from b in buckets select b.CompanyName).ToArray();

                return list;
            }
        }
        public string[] GetUsers(string endpoint, string companyName, string username)
        {
            using (var repository = new LogElasticsearchRepository())
            {
                var query = new QueryBuilder()
                    .AddMatchPhraseQuery(companyName, "companyName")
                    .AddPrefixMatchQuery(username, "username")
                    .AddMatchPhraseQuery(endpoint, "endpoint")
                    .Build();

                var request = repository.CreateSearchBuilder()
                    .AddQuery(query)
                    .Build();

                var result = repository.Search(request);

                var buckets = result.Documents;

                var list = (from b in buckets select b.Username).ToArray();

                return list;
            }
        }
    }
}
