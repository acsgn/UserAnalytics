using RabbitMQ.Client;

namespace KariyerAnalytics.Data.Contract
{
    public interface IRabbitMQContext
    {
        IModel GetRabbitMQClient();
    }
}