using System;
using System.Configuration;
using RabbitMQ.Client;

namespace UserAnalytics.Data
{
    sealed class RabbitMQConnection
    {
        private static ConnectionFactory _ConnectionFactory;
        static RabbitMQConnection()
        {
            var uri = new Uri(ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString);
            _ConnectionFactory = new ConnectionFactory() { Uri = uri };
        }

        public static IConnection CreateConnection()
        {
            return _ConnectionFactory.CreateConnection(); 
        }
    }
}
