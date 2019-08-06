using Nest;

namespace KariyerAnalytics.Data
{
    sealed class ElasticsearchConnection
    {
        public static ConnectionSettings ConnectionSettings { get; }
        static ElasticsearchConnection()
        {
            ConnectionSettings = new ConnectionSettings();//new Uri(ConfigurationManager.AppSettings["ElasticsearchUri"]));
        }
    }
}
