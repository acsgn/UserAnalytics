using System;
using System.Configuration;
using Nest;

namespace KariyerAnalytics.Data
{
    sealed class DefaultConnectionSettings
    {
        private static ConnectionSettings connectionSettings;

        public static ConnectionSettings GetConnectionSettings()
        {
            if (connectionSettings == null)
            {
                connectionSettings = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["ElasticsearchUri"]));
            }
            return connectionSettings;
        }

    }
}
