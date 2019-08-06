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
        public void Queue(string routingKey, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(json);
            RabbitMQContext.Channel.BasicPublish(
                    exchange: "",
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);
        }
        public void Dequeue(string routingKey, Func<T, bool> target)
        {
            var consumer = new GenericRabbitMQConsumer<T>(RabbitMQContext.Channel, target);
            RabbitMQContext.Channel.BasicConsume(
                queue: routingKey,
                consumer: consumer);
        }

        public void BulkDequeue(string routingKey, int bulk, Func<IEnumerable<T>, bool> func)
        {
            var consumer = new GenericBulkRabbitMQConsumer<T>(bulk, func, RabbitMQContext.Channel);
            RabbitMQContext.Channel.BasicConsume(
                queue: routingKey,
                consumer: consumer);
        }

        public void CreateQueue(string routingKey)
        {
            RabbitMQContext.Channel.QueueDeclare(
                queue: routingKey,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
    }
}
