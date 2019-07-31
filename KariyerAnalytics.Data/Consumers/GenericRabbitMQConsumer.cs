using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace KariyerAnalytics.Data
{
    public class GenericRabbitMQConsumer<T> : EventingBasicConsumer where T : class
    {
        private readonly Func<T, bool> _Add;
        
        public GenericRabbitMQConsumer(IModel model, Func<T, bool> target) : base(model)
        {
            _Add = target;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            var json = Encoding.UTF8.GetString(body);
            var document = JsonConvert.DeserializeObject<T>(json);

            var ack = _Add(document);
            while (!ack)
            {
                ack = _Add(document);
            }
            Model.BasicAck(deliveryTag, false);
        }
    }
}
