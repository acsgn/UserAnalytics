using System;
using System.Configuration;

namespace KariyerAnalytics.Data
{
    sealed class RabbitMQConnectionSettingsSingleton
    {
        private static Uri _ConnectionURL;

        public static Uri GetConnectionSettings()
        {
            if (_ConnectionURL == null)
            {
                _ConnectionURL = new Uri(ConfigurationManager.AppSettings["RabbitMQhUri"]);
            }
            return _ConnectionURL;
        }

    }
}
