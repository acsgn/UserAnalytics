using Nest;

namespace KariyerAnalytics.Data
{
    public sealed class ElasticsearchConnectionSettings
    {
        public ConnectionSettings GetDefaultConnectionSettings()
        {
            var connectionSettings = new ConnectionSettings();//new Uri(ConfigurationManager.AppSettings["ElasticsearchUri"]));
            return connectionSettings;
        }
    }
}
