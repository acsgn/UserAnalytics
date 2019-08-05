using System;
using System.Collections.Generic;
using System.Text;
using KariyerAnalytics.Data.Contract;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace KariyerAnalytics.Data.Repositories
{
    public class GenericRabbitMQRepository<T> : IGenericRabbitMQRepository<T> where T : class
    {
        private IRabbitMQContext _RabbitMQContext;
        public GenericRabbitMQRepository(IRabbitMQContext context)
        {
            _RabbitMQContext = context;
        }
        public void Queue(string routingKey, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(json);

            _RabbitMQContext.GetRabbitMQClient().BasicPublish(
                    exchange: "",
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);
        }
        public void Dequeue(string routingKey, Func<T, bool> target)
        {
            var client = _RabbitMQContext.GetRabbitMQClient();
            var consumer = new GenericRabbitMQConsumer<T>(client, target);
            client.BasicConsume(
                queue: routingKey,
                consumer: consumer);
        }

        public void BulkDequeue(string routingKey, int bulk, Func<IEnumerable<T>, bool> func)
        {
            var client = _RabbitMQContext.GetRabbitMQClient();
            var consumer = new GenericBulkRabbitMQConsumer<T>(bulk, func, client);
            client.BasicConsume(
                queue: routingKey,
                consumer: consumer);
        }

        public void CreateQueue(string routingKey)
        {
            _RabbitMQContext.GetRabbitMQClient().QueueDeclare(
                queue: routingKey,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
    }
}
