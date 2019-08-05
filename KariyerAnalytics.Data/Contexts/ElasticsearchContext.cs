using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data
{
    public class ElasticsearchContext : IElasticsearchContext
    {
        private readonly static ElasticsearchConnectionSettings _ConnectionSettings;

        static ElasticsearchContext()
        {
            _ConnectionSettings = new ElasticsearchConnectionSettings();
        }

        private readonly ElasticClient _ElasticClient;

        public ElasticsearchContext()
        {
            _ElasticClient = new ElasticClient();// _ConnectionSettings.GetDefaultConnectionSettings());
        }

        public ElasticClient GetElasticClient()
        {
            return _ElasticClient;
        }
    }
}
