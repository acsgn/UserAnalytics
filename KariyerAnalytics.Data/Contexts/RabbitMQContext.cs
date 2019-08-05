using KariyerAnalytics.Data.Contract;
using RabbitMQ.Client;

namespace KariyerAnalytics.Data
{
    public class RabbitMQContext : IRabbitMQContext
    {
        private readonly static ConnectionFactory _ConnectionFactory;

        static RabbitMQContext()
        {
            _ConnectionFactory = new ConnectionFactory() { HostName = "localhost" };
        }

        private readonly IConnection _Connection;

        public RabbitMQContext()
        {
            _Connection = _ConnectionFactory.CreateConnection();
        }

        public IModel GetRabbitMQClient()
        {
            return _Connection.CreateModel();
        }
    }
}
