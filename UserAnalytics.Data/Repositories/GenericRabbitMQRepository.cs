using System;
using System.Collections.Generic;
using System.Text;
using UserAnalytics.Data.Contract;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace UserAnalytics.Data.Repositories
{
    public class GenericRabbitMQRepository<T> : IGenericRabbitMQRepository<T> where T : class
    {
        private string _ConsumerTag;
        public bool Queue(string routingKey, T obj)
        {
            if (RabbitMQContext.Channel.IsOpen)
            {
                var json = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(json);
                RabbitMQContext.Channel.BasicPublish(
                        exchange: "",
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body);
                return true;
            }
            else return false;
        }
        public void Dequeue(string routingKey, Func<T, bool> target)
        {
            var consumer = new GenericRabbitMQConsumer<T>(RabbitMQContext.Channel, target);
            _ConsumerTag = RabbitMQContext.Channel.BasicConsume(
                queue: routingKey,
                consumer: consumer);
        }

        public void BulkDequeue(string routingKey, int bulk, Func<IEnumerable<T>, bool> func)
        {
            var consumer = new GenericBulkRabbitMQConsumer<T>(bulk, func, RabbitMQContext.Channel);
            _ConsumerTag = RabbitMQContext.Channel.BasicConsume(
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

        public bool CheckConnection()
        {
            return RabbitMQContext.Channel.IsOpen;
        }

        public void StopConsumer()
        {
            if(_ConsumerTag != null)
            {
                RabbitMQContext.Channel.BasicCancel(_ConsumerTag);
            }
        }
    }
}
