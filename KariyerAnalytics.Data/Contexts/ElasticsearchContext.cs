using Nest;

namespace UserAnalytics.Data
{
    public class ElasticsearchContext
    {
        public static ElasticClient ElasticClient { get; private set; }

        static ElasticsearchContext()
        {
            ElasticClient = new ElasticClient(ElasticsearchConnection.ConnectionSettings);
        }

        public static void CreateChannel()
        {
            ElasticClient = new ElasticClient(ElasticsearchConnection.ConnectionSettings);
        }

    }
}
