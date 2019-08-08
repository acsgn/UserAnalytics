using System;
using System.Configuration;
using Nest;

namespace KariyerAnalytics.Data
{
    sealed class ElasticsearchConnection
    {
        public static ConnectionSettings ConnectionSettings { get; }
        static ElasticsearchConnection()
        {
            var uri = new Uri(ConfigurationManager.ConnectionStrings["Elasticsearch"].ConnectionString);
            ConnectionSettings = new ConnectionSettings(uri);
        }
    }
}
