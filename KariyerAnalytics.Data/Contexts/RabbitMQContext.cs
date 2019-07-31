using System;
using KariyerAnalytics.Data.Contract;
using RabbitMQ.Client;

namespace KariyerAnalytics.Data
{
    public class RabbitMQContext : IRabbitMQContext, IDisposable
    {
        private readonly IModel _RabbitMQClient;
        public RabbitMQContext()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _RabbitMQClient = connection.CreateModel();
        }

        public IModel GetRabbitMQClient()
        {
            return _RabbitMQClient;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
