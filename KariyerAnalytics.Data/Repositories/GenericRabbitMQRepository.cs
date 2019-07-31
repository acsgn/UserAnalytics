using System;
using System.Text;
using KariyerAnalytics.Data.Contract;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace KariyerAnalytics.Data.Repositories
{
    public class GenericRabbitMQRepository<T> : IGenericRabbitMQRepository<T>, IDisposable where T : class
    {
        public void Queue(string routingKey, T obj)
        {
            using (var context = new RabbitMQContext())
            {
                var json = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(json);

                context.GetRabbitMQClient().BasicPublish(
                    exchange: "",
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);
            }
        }
        public void Dequeue(string routingKey, Func<T, bool> target)
        {
            using (var context = new RabbitMQContext())
            {
                var consumer = new GenericRabbitMQConsumer<T>(context.GetRabbitMQClient(), target);
                context.GetRabbitMQClient().BasicConsume(
                    queue: routingKey,
                    consumer: consumer);
            }
        }

        public void BulkDequeue(string routingKey, int bulk)
        {
            using (var context = new RabbitMQContext())
            {
                var consumer = new GenericBulkRabbitMQConsumer<T>(bulk, context.GetRabbitMQClient());
                context.GetRabbitMQClient().BasicConsume(
                    queue: routingKey,
                    consumer: consumer);
            }
        }

        public void CreateQueue(string routingKey)
        {
            using (var context = new RabbitMQContext())
            {
                context.GetRabbitMQClient().QueueDeclare(
                    queue: routingKey,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
