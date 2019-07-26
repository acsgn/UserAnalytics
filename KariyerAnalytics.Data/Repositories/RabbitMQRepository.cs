using System;
using System.Text;
using KariyerAnalytics.Data.Contract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace KariyerAnalytics.Data.Repositories
{
    public class RabbitMQRepository<T> : IRabbitMQRepository<T>, IDisposable where T : class
    {
        public void Queue(string routingKey, T obj)
        {
            using (var context = new RabbitMQContext())
            {
                var json = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(json);

                context.GetRabbitMQClient().BasicPublish(exchange: "",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
            }
        }
        
        public void Dequeue(string routingKey)
        {
            using (var context = new RabbitMQContext())
            {
                var consumer = new EventingBasicConsumer(context.GetRabbitMQClient());
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var json = Encoding.UTF8.GetString(body);
                    var obj = JsonConvert.DeserializeObject(json, typeof(T));
                    // Here we have our T object again
                };
                context.GetRabbitMQClient().BasicConsume(queue: routingKey,
                                     autoAck: true,
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
