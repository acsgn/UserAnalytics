using System;
using System.Configuration;
using Nest;

namespace KariyerAnalytics.Data
{
    sealed class ElasticsearchConnectionSettings
    {
        private static ConnectionSettings connectionSettings;

        public static ConnectionSettings GetDefaultConnectionSettings()
        {
            if (connectionSettings == null)
            {
                connectionSettings = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["ElasticsearchUri"]));
            }
            return connectionSettings;
        }

    }
}
