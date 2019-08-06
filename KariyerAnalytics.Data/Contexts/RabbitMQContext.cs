using RabbitMQ.Client;

namespace KariyerAnalytics.Data
{
    public class RabbitMQContext
    {
        private static IConnection Connection;
        static RabbitMQContext()
        {
            Connection = RabbitMQConnection.CreateConnection();
            CreateChannel();
        }

        public static IModel Channel { get; private set; }
        
        public static void CreateChannel()
        {
            Channel = Connection.CreateModel();
        }
    }
}
