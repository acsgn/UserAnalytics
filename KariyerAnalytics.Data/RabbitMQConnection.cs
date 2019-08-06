using System;
using System.Configuration;
using RabbitMQ.Client;

namespace KariyerAnalytics.Data
{
    sealed class RabbitMQConnection
    {
        private static ConnectionFactory _ConnectionFactory;
        static RabbitMQConnection()
        {
            _ConnectionFactory = new ConnectionFactory() { HostName = "localhost" };
        }

        public static IConnection CreateConnection()
        {
            return _ConnectionFactory.CreateConnection(); 
        }
    }
}
