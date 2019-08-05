using System;
using System.Configuration;

namespace KariyerAnalytics.Data
{
    sealed class RabbitMQConnectionSettings
    {
        public void GetConnectionSettings()
        {
            return; //new Uri(ConfigurationManager.AppSettings["RabbitMQUri"]);
        }
    }
}
